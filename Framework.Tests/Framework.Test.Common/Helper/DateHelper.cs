using System;

namespace Framework.Test.Common.Helper
{
    public static class DateHelper
    {
        public static string ToDateTimeString(this DateTime date, string dateFormat = "yyyy-MM-dd")
        {
            return date.ToString(dateFormat);
        }

        public static string GetCurrentDateString(string dateFormat, string timeLine = null, string timeLineFormat = null)
        {
            DateTime currentDate = DateTime.Now;

            return GetTargetDateString(currentDate, dateFormat, timeLine, timeLineFormat);
        }

        public static string GetNextDateString(string dateFormat, string timeLine = null, string timeLineFormat = null)
        {
            return GetNextNDateString(dateFormat, timeLine, timeLineFormat, 1);
        }

        public static string GetNextNDateString(string dateFormat, string timeLine = null, string timeLineFormat = null, int addDays = 0)
        {
            DateTime nextDate = DateTime.Now.AddDays(addDays);

            return GetTargetDateString(nextDate, dateFormat, timeLine, timeLineFormat);
        }

        public static string GetTargetDateString(DateTime targetDate, string dateFormat, string timeLine = null, string timeLineFormat = null)
        {
            DateTime currentDate = targetDate;

            if (timeLine !=  null)
            {
                DateTime timeLineDate;

                if (timeLineFormat != null)
                {
                    timeLineDate = DateTime.ParseExact(timeLine, timeLineFormat, null);
                }
                else
                {
                    timeLineDate = DateTime.ParseExact(timeLine, dateFormat, null);
                }

                if (DateTime.Compare(currentDate, timeLineDate) < 0)
                {
                    return timeLineDate.ToString(dateFormat);
                }
            }

            return currentDate.ToString(dateFormat);
        }

        public static DateTime GetDateFromString(string date, string dateFormat)
        {
            return DateTime.ParseExact(date, dateFormat, null);
        }
    }
}
