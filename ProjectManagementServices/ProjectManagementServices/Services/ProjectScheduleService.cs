using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using DlhSoft.ComponentModel;

namespace ProjectManagementServices
{
    public class ProjectScheduleService
    {
        public ScheduleOutput Schedule(ScheduleInput input)
        {
            string projectXml = input.ProjectXml;

            var taskManager = Provider.GetTaskManager(projectXml);

            var projectStart = taskManager.GetProjectStart();
            if (projectStart == DateTime.MinValue || projectStart == DateTime.MaxValue)
                projectStart = DateTime.Today;

            if (input.EnsureValidHierarchy)
                taskManager.EnsureValidHierarchy();
            if (input.EnsureTimeLimitConstraints)
                taskManager.EnsureTimeLimitConstraints();
            if (input.RemoveCircularDependencies)
                taskManager.RemoveCircularDependencies();
            if (input.EnsureDependencyConstraints)
                taskManager.EnsureDependencyConstraints(includeStartedTasks: true, start: projectStart);
            if (input.SummarizeParentTimeValues)
                taskManager.SummarizeParentTimeValues();

            if (input.SplitAllRemainingWork)
                taskManager.SplitAllRemainingWork();
            if (input.OptimizeWork)
                taskManager.OptimizeWork(includeStarted: true, start: projectStart);
            if (input.LevelAllocations)
                taskManager.LevelAllocations();
            if (input.LevelResources)
                taskManager.LevelResources(includeStartedTasks: true, start: projectStart);

            projectXml = taskManager.GetProjectXml();

            return new ScheduleOutput { ProjectXml = projectXml };
        }

        public class ScheduleInput : BaseInput
        {
            /// <summary>
            /// Resets any invalid indentation values in order to obtain a valid task hierarchy.
            /// </summary>
            public bool EnsureValidHierarchy { get; set; }

            /// <summary>
            /// Updates standard task time values according to the general schedule.
            /// </summary>
            public bool EnsureTimeScheduleConstraints { get; set; }

            /// <summary>
            /// Updates standard task time values according to the item's minimum and maximum start and finish values.
            /// </summary>
            public bool EnsureTimeLimitConstraints { get; set; }

            /// <summary>
            /// Removes predecessor items that generate circular dependencies, either explicitely or implicitely in the hierarchy.
            /// </summary>
            public bool RemoveCircularDependencies { get; set; }

            /// <summary>
            /// Ensures that dependency constrains between tasks are enforced.
            /// </summary>
            public bool EnsureDependencyConstraints { get; set; }

            /// <summary>
            /// Updates parent task time values according to the child item time values.
            /// </summary>
            public bool SummarizeParentTimeValues { get; set; }

            /// <summary>
            /// Creates and inserts partial copies of the standard started task items that have not yet been fully completed considering their remaining work effort into the managed hierarchy, and updates the finish date and times of the original task items to their completion points.
            /// Used as preparation for rescheduling the remaining work effort task items, such as before leveling resources.
            /// </summary>
            public bool SplitAllRemainingWork { get; set; }

            /// <summary>
            /// Optimizes schedule times of all managed items in order to optimize the project finish date without leveling resources, respecting dependency constraints.
            /// </summary>
            public bool OptimizeWork { get; set; }

            /// <summary>
            /// Levels the assigned allocation units assuming that all tasks within the component are fixed duration and effort driven.
            /// </summary>
            public bool LevelAllocations { get; set; }

            /// <summary>
            /// Levels the assigned resources from all standard tasks within the component in order to avoid over allocation, by updating task timing values accordingly.
            /// The resource leveling algorithm is applied starting from the current date and time, and without considering already started tasks.
            /// </summary>
            public bool LevelResources { get; set; }
        }

        public class ScheduleOutput : BaseOutput { }
    }
}
