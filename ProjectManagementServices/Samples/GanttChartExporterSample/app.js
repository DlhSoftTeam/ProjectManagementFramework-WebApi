/// <reference path='./Scripts/DlhSoft.ProjectData.GanttChart.HTML.Controls.d.ts'/>
/// <reference path='./Scripts/DlhSoft.ProjectData.GanttChart.HTML.Controls.Extras.d.ts'/>
var GanttChartView = DlhSoft.Controls.GanttChartView;
var ProjectSerializer = DlhSoft.Controls.GanttChartView.ProjectSerializer;
var ganttChartView;
window.onload = function () {
    // Access user interface elements.
    var ganttChartViewElement = document.getElementById("ganttChartView");
    // Prepare data items.
    var items = [
        { content: "Task 1", start: new Date(2013, 8 - 1, 22, 08), finish: new Date(2013, 8 - 1, 23, 12), completedFinish: new Date(2013, 8 - 1, 22, 12), assignmentsContent: "Resource 1" },
        { content: "Task 2", start: new Date(2013, 8 - 1, 23, 08), finish: new Date(2013, 8 - 1, 23, 16) }];
    for (var i = 3; i <= 10; i++)
        items.push({ content: "Task " + i, start: new Date(2013, 8 - 1, 22 + i - 3, 08), finish: new Date(2013, 8 - 1, 23 + i - 3, 16) });
    items[1].predecessors = [{ item: items[0], dependencyType: "FS" }];
    // Prepare configuration settings.
    var settings = {
        currentTime: new Date(2013, 8 - 1, 22),
        areTaskDependencyConstraintsEnabled: true // Automatically reschedule tasks based on their dependencies.
    };
    // Initialize component.
    ganttChartView = GanttChartView.initialize(ganttChartViewElement, items, settings);
};
function initializeGanttChartExporterInput() {
    // Access user interface elements.
    var projectXmlInputElement = document.getElementById("projectXmlInput");
    var timelineStartInputElement = document.getElementById("timelineStartInput");
    var timelineFinishInputElement = document.getElementById("timelineFinishInput");
    // Prepare Project XML data.
    var projectSerializer = ProjectSerializer.initialize(ganttChartView);
    projectXmlInputElement.value = encodeURIComponent(projectSerializer.getXml());
}
//# sourceMappingURL=app.js.map