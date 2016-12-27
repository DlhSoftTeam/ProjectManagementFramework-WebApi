using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using DlhSoft.ComponentModel;

namespace ProjectManagementServices
{
    public class GanttChartExporterService
    {
        public GetImageBytesOutput GetImageBytes(GetImageBytesInput input)
        {
            string projectXml = input.ProjectXml;
            DateTime timelineStart = input.TimelineStart;
            DateTime timelineFinish = input.TimelineFinish;
            double resolution = input.Resolution;

            var taskManager = Provider.GetTaskManager(projectXml);
            Provider.PrepareExport(ref timelineStart, ref timelineFinish, ref resolution, taskManager);

            projectXml = taskManager.GetProjectXml();

            var ganttChartExporter = new DlhSoft.Windows.Services.GanttChartExporter(taskManager, control =>
            {
                control.Columns = new DlhSoft.Windows.Controls.DataGridColumnCollection {
                    new DlhSoft.Windows.Controls.DataTreeGridColumn { Header = "Task", MinWidth = 200 }
                };
                control.SetTimelinePage(timelineStart, timelineFinish);
            });
            var imageBytes = ganttChartExporter.GetImageBytes(resolution);
            
            return new GetImageBytesOutput { ProjectXml = projectXml, ImageBytes = imageBytes };
        }

        public class GetImageBytesInput : BaseInput
        {
            /// <summary>
            /// The start date and time of the exported chart.
            /// </summary>
            public DateTime TimelineStart { get; set; }

            private DateTime timelineFinish = DateTime.MaxValue;

            /// <summary>
            /// The end date and time of the exported chart.
            /// </summary>
            public DateTime TimelineFinish { get { return timelineFinish; } set { timelineFinish = value; } }

            /// <summary>
            /// The resolution to export image at.
            /// </summary>
            public double Resolution { get; set; }
        }

        public class GetImageBytesOutput : BaseOutput
        {
            /// <summary>
            /// The exported PNG image bytes.
            /// </summary>
            public byte[] ImageBytes { get; set; }
        }
    }
}
