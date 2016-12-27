/// <reference path='./Scripts/DlhSoft.ProjectData.GanttChart.HTML.Controls.d.ts'/>
/// <reference path='./Scripts/DlhSoft.ProjectData.GanttChart.HTML.Controls.Extras.d.ts'/>
import GanttChartView = DlhSoft.Controls.GanttChartView;
import TaskItem = GanttChartView.Item;
import PredecessorItem = GanttChartView.PredecessorItem;
import ProjectSerializer = DlhSoft.Controls.GanttChartView.ProjectSerializer;

declare var $;

var ganttChartView: GanttChartView.Element;

window.onload = () => {
    // Access user interface elements.
    var ganttChartViewElement = document.getElementById("ganttChartView");

    // Prepare data items.
    var now = new Date(), year = now.getFullYear(), month = now.getMonth() + 1;
    var items = <TaskItem[]>[
        { content: "Task 1", start: new Date(year, month, 2, 08), finish: new Date(year, month, 3, 12), completedFinish: new Date(year, month, 2, 12), assignmentsContent: "Resource 1" },
        { content: "Task 2", start: new Date(year, month, 3, 08), finish: new Date(year, month, 3, 16) }];
    for (var i = 3; i <= 10; i++)
        items.push({ content: "Task " + i, start: new Date(year, month, 2, 08), finish: new Date(year, month, 3, 16), assignmentsContent: "Resource " + (2 + i % 2) });
    items[1].predecessors = <PredecessorItem[]>[{ item: items[0], dependencyType: "FS" }];

    // Prepare configuration settings.
    var settings = <GanttChartView.Settings>{
        areTaskDependencyConstraintsEnabled: true // Automatically reschedule tasks based on their dependencies.
    };

    // Initialize component.
    ganttChartView = GanttChartView.initialize(ganttChartViewElement, items, settings);
};

function levelResources() {
    // Prepare Project XML data.
    var projectSerializer = ProjectSerializer.initialize(ganttChartView);
    var projectXml = projectSerializer.getXml();

    // Call DlhSoft Project Management Services using JQuery.
    $.ajax({
        url: "http://localhost:62534/API/ProjectSchedule",
        method: "POST",
        data: {
            ProjectXml: projectXml,
            EnsureValidHierarchy: false,
            EnsureTimeScheduleConstraints: false,
            EnsureTimeLimitConstraints: false,
            RemoveCircularDependencies: false,
            EnsureDependencyConstraints: false,
            SummarizeParentTimeValues: false,
            SplitAllRemainingWork: false,
            OptimizeWork: false,
            LevelAllocations: false,
            LevelResources: true
        },
        success: (response) => {
            projectXml = response.ProjectXml;
            projectSerializer.loadXml(projectXml);
        },
        error: () => { alert("An error has occurred."); }
    });
}
