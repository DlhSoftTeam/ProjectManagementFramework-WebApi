using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectManagementServices
{
    public partial class GanttChartExporter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string projectXml = Request.Params["ProjectXml"] ?? "<?xml version='1.0' encoding='UTF-8' standalone='yes'?> <Project xmlns='http://schemas.microsoft.com/project'> <SaveVersion>14</SaveVersion> <ScheduleFromStart>1</ScheduleFromStart> <StartDate>11970-03-20T00:00:00</StartDate> <DefaultStartTime>08:00:00</DefaultStartTime> <DefaultFinishTime>16:00:00</DefaultFinishTime> <MinutesPerDay>480</MinutesPerDay> <MinutesPerWeek>2400</MinutesPerWeek> <DurationFormat>7</DurationFormat> <WorkFormat>2</WorkFormat> <CalendarUID>1</CalendarUID> <Calendars> <Calendar> <UID>1</UID> <Name>Standard</Name> <IsBaseCalendar>1</IsBaseCalendar> <BaseCalendarUID>-1</BaseCalendarUID> <WeekDays> <WeekDay> <DayType>1</DayType> <DayWorking>0</DayWorking> </WeekDay> <WeekDay> <DayType>2</DayType> <DayWorking>1</DayWorking> <WorkingTimes> <WorkingTime> <FromTime>08:00:00</FromTime> <ToTime>16:00:00</ToTime> </WorkingTime> </WorkingTimes> </WeekDay> <WeekDay> <DayType>3</DayType> <DayWorking>1</DayWorking> <WorkingTimes> <WorkingTime> <FromTime>08:00:00</FromTime> <ToTime>16:00:00</ToTime> </WorkingTime> </WorkingTimes> </WeekDay> <WeekDay> <DayType>4</DayType> <DayWorking>1</DayWorking> <WorkingTimes> <WorkingTime> <FromTime>08:00:00</FromTime> <ToTime>16:00:00</ToTime> </WorkingTime> </WorkingTimes> </WeekDay> <WeekDay> <DayType>5</DayType> <DayWorking>1</DayWorking> <WorkingTimes> <WorkingTime> <FromTime>08:00:00</FromTime> <ToTime>16:00:00</ToTime> </WorkingTime> </WorkingTimes> </WeekDay> <WeekDay> <DayType>6</DayType> <DayWorking>1</DayWorking> <WorkingTimes> <WorkingTime> <FromTime>08:00:00</FromTime> <ToTime>16:00:00</ToTime> </WorkingTime> </WorkingTimes> </WeekDay> <WeekDay> <DayType>7</DayType> <DayWorking>0</DayWorking> </WeekDay> </WeekDays> </Calendar> </Calendars> <Tasks> </Tasks> <Resources> </Resources> <Assignments> </Assignments> </Project>";
            DateTime timelineStart = DateTime.MinValue; DateTime.TryParse(Request.Params["TimelineStart"], out timelineStart);
            DateTime timelineFinish = DateTime.MaxValue; DateTime.TryParse(Request.Params["TimelineFinish"], out timelineFinish);
            double resolution; double.TryParse(Request.Params["Resolution"], out resolution);

            var ganttChartExporterService = new GanttChartExporterService();
            var getImageBytesInput = new GanttChartExporterService.GetImageBytesInput { ProjectXml = projectXml, TimelineStart = timelineStart, TimelineFinish = timelineFinish, Resolution = resolution };
            var getImageBytesOutput = ganttChartExporterService.GetImageBytes(getImageBytesInput);

            var imageBytes = getImageBytesOutput.ImageBytes;

            Response.ContentType = "image/png";
            Response.BinaryWrite(imageBytes);
            Response.End();
        }
    }
}
