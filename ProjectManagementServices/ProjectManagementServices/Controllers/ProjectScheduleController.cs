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
    public class ProjectScheduleController : ApiController
    {
        /// <summary>
        /// Schedules the specified input project data by applying the specified operations and using the specified settings.
        /// Project size is limited to 1024 tasks.
        /// </summary>
        [HttpPost]
        public ProjectScheduleService.ScheduleOutput Schedule([FromBody] ProjectScheduleService.ScheduleInput input)
        {
            var projectScheduleService = new ProjectScheduleService();
            return projectScheduleService.Schedule(input);
        }
    }
}
