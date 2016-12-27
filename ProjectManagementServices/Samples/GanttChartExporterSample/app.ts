/// <reference path='./Scripts/DlhSoft.ProjectData.GanttChart.HTML.Controls.d.ts'/>
/// <reference path='./Scripts/DlhSoft.ProjectData.GanttChart.HTML.Controls.Extras.d.ts'/>
import GanttChartView = DlhSoft.Controls.GanttChartView;
import TaskItem = GanttChartView.Item;
import PredecessorItem = GanttChartView.PredecessorItem;
import ProjectSerializer = DlhSoft.Controls.GanttChartView.ProjectSerializer;

var ganttChartView: GanttChartView.Element;

window.onload = () => {
    // Access user interface elements.
    var ganttChartViewElement = document.getElementById("ganttChartView");

    // Prepare data items.
    var items = <TaskItem[]>[
        { content: "Task 1", start: new Date(2013, 8 - 1, 22, 08), finish: new Date(2013, 8 - 1, 23, 12), completedFinish: new Date(2013, 8 - 1, 22, 12), assignmentsContent: "Resource 1" },
        { content: "Task 2", start: new Date(2013, 8 - 1, 23, 08), finish: new Date(2013, 8 - 1, 23, 16) }];
    for (var i = 3; i <= 10; i++)
        items.push({ content: "Task " + i, start: new Date(2013, 8 - 1, 22 + i - 3, 08), finish: new Date(2013, 8 - 1, 23 + i - 3, 16) });
    items[1].predecessors = <PredecessorItem[]>[{ item: items[0], dependencyType: "FS" }];

    // Prepare configuration settings.
    var settings = <GanttChartView.Settings>{
        currentTime: new Date(2013, 8 - 1, 22), // Display the current time vertical line of the chart at the project start date.
        areTaskDependencyConstraintsEnabled: true // Automatically reschedule tasks based on their dependencies.
    };

    // Initialize component.
    ganttChartView = GanttChartView.initialize(ganttChartViewElement, items, settings);
};

function initializeGanttChartExporterInput() {
    // Access user interface elements.
    var projectXmlInputElement = <HTMLInputElement>document.getElementById("projectXmlInput");
    var timelineStartInputElement = <HTMLInputElement>document.getElementById("timelineStartInput");
    var timelineFinishInputElement = <HTMLInputElement>document.getElementById("timelineFinishInput");

    // Prepare Project XML data.
    var projectSerializer = ProjectSerializer.initialize(ganttChartView);
    projectXmlInputElement.value = encodeURIComponent(projectSerializer.getXml());
}
