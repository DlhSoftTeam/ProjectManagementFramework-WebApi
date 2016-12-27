using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjectManagementServices
{
    public static class Provider
    {
        public static DlhSoft.Windows.Data.TaskManager GetTaskManager(string projectXml)
        {
            if (projectXml == null)
                throw new ArgumentNullException("projectXml");
            if (!projectXml.Contains('<'))
                projectXml = HttpUtility.UrlDecode(projectXml);

            var taskManager = new DlhSoft.Windows.Data.TaskManager();
            taskManager.LoadProjectXml(projectXml);

            var maxCount = 1024;
            if (taskManager.Items.Count > maxCount)
            {
                while (maxCount > 0 && maxCount + 1 < taskManager.Items.Count && taskManager.Items[maxCount].Indentation < taskManager.Items[maxCount + 1].Indentation)
                    maxCount--;
                taskManager.Items.RemoveRange(maxCount, taskManager.Items.Count - maxCount);
            }

            return taskManager;
        }

        public static void PrepareExport(ref DateTime timelineStart, ref DateTime timelineFinish, ref double resolution, DlhSoft.Windows.Data.TaskManager taskManager)
        {
            if (timelineStart == DateTime.MinValue)
                timelineStart = taskManager.GetProjectStart();
            if (timelineFinish == DateTime.MaxValue || timelineFinish == DateTime.MinValue)
                timelineFinish = taskManager.GetProjectFinish();
            if (timelineStart == DateTime.MaxValue)
                timelineStart = DateTime.Today;
            if (timelineFinish == DateTime.MinValue)
                timelineFinish = DateTime.Today.AddDays(1);
            if (timelineFinish < timelineStart)
                timelineFinish = timelineStart;
            if ((timelineFinish - timelineStart).TotalDays > 366)
                timelineFinish = timelineStart.AddDays(366);
            while (timelineStart.DayOfWeek != DayOfWeek.Sunday)
                timelineStart = timelineStart.AddDays(-1);
            while (timelineFinish.DayOfWeek != DayOfWeek.Sunday)
                timelineFinish = timelineFinish.AddDays(1);

            if (double.IsNaN(resolution) || resolution <= 0 || resolution > 384)
                resolution = 192;
        }
    }
}