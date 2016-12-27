using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;

namespace ProjectManagementServices.Controllers
{
    public class GanttChartExporterController : ApiController
    {
        /// <summary>
        /// Generates and returns the bytes of a PNG based Gantt Chart image for the specified input project data using the specified settings.
        /// Exported project size is limited to 1024 tasks, timeline duration is limited to 366 days, and image resolution is limited to 384 dots per inch.
        /// Alternatively, to obtain image bytes directly, post form with similar input parameters to GanttChartExporter.aspx.
        /// </summary>
        [HttpPost]
        public GanttChartExporterService.GetImageBytesOutput GetImageBytes([FromBody] GanttChartExporterService.GetImageBytesInput input)
        {
            var ganttChartExporterService = new GanttChartExporterService();
            return ganttChartExporterService.GetImageBytes(input);
        }
    }
}
