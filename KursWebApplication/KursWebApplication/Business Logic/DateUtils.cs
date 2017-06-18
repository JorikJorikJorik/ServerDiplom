using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace KursWebApplication.Business_Logic
{
    public class DateUtils
    {
        private int year;

        public DateUtils()
        {
        }

        public DateUtils(int year)
        {
            this.year = year;
        }


        public int getWeekNumberByDateTime(DateTime date)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            return currentCulture.Calendar.GetWeekOfYear(date, currentCulture.DateTimeFormat.CalendarWeekRule, currentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public DateTime firstDateOfWeekISO8601(int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }

        public string convertDateToWeekIntervalLable(DateTime date)
        {
            string startWeek = String.Format("{0}.{1}", formatWeekDate(date.Day), formatWeekDate(date.Month));
            var endDate = calculateEndWeekMounth(date);
            string endtWeek = String.Format("{0}.{1}", formatWeekDate(endDate.Day), formatWeekDate(endDate.Month));
            return String.Format("{0}-{1}", startWeek, endtWeek);
        }

        private String formatWeekDate(int dateField) {
            return String.Format("{0}{1}", dateField.ToString().Count() > 1 ? "" : "0", dateField);
        }

        public DateTime calculateEndWeekMounth(DateTime date)
        {
            int year1 = 2017;
            int daysCount = DateTime.DaysInMonth(date.Year, date.Month);
            bool checkCountDayInMonth = date.Day + 6 > daysCount;
            int month = checkCountDayInMonth ? (date.Month < 12 ? date.Month + 1 : 1) : date.Month;
            int year = checkCountDayInMonth && date.Month < 12 ? year1 : year1++;
            int endWeekDay = checkCountDayInMonth ? date.Day + 6 - daysCount : date.Day + 6;
            return new DateTime(year, month, endWeekDay);
        }

        public DateTime calculateRightNextMonth(DateTime dateStart)
        {
            int startMonth = dateStart.Month;
            return startMonth + 1 > 12 ? new DateTime(dateStart.Year, dateStart.Month, 31) : new DateTime(dateStart.Year, dateStart.Month + 1, dateStart.Day);
        }

        public DateTime calculateRightNextWeek(DateTime dateStart)
        {
            int startDay = dateStart.Day;
            int daysCount = DateTime.DaysInMonth(dateStart.Year, dateStart.Month);
            bool checkCountDayInMonth = dateStart.Day + 6 > daysCount;
            return checkCountDayInMonth ? new DateTime(dateStart.Year, dateStart.Month, 31) : new DateTime(dateStart.Year, dateStart.Month, dateStart.Day + 6);
        }

        public int[] getPeriodDayMonthByWeekNumber(int weekNumber)
        {
            DateTime startDate = firstDateOfWeekISO8601(weekNumber);
            DateTime finishDate = calculateEndWeekMounth(startDate);

            if (startDate.Month == finishDate.Month)
            {
                return Enumerable.Range(startDate.Day, finishDate.Day - startDate.Day + 1).ToArray();
            }
            else
            {
                int countDayInStartMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                int[] firstPart = Enumerable.Range(startDate.Day, countDayInStartMonth - startDate.Day + 1).ToArray();
                int[] secondPart = Enumerable.Range(1, finishDate.Day).ToArray();

                var result = new int[firstPart.Length + secondPart.Length];
                firstPart.CopyTo(result, 0);
                secondPart.CopyTo(result, firstPart.Length);

                return result;
            }
        }

        public DateTime UnixTimeStampToDateTime(Int32 timestamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(timestamp).ToLocalTime();
            return dtDateTime;
        }

        public int DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            return (Int32)(dateTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds;
        }

        public string stateLastActivityProfile(int profileId)
        {
            var db = new MyDBModels.DB();
            var profileModel = db.profile.Where(p => p.ProfileId == profileId).First();
            var lastActive = profileModel.TimeLastActive;

            return calculateStateLastActivity(lastActive);
        }

        public string calculateStateLastActivity(DateTime lastActive)
        {
            var lastActiveMilis = (Int32)(lastActive.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds;
            var dateNowMillis = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds;

            var dateDifferent = UnixTimeStampToDateTime((Int32)(dateNowMillis - lastActiveMilis - 7200));

            if (dateDifferent.Year > 1970 || dateDifferent.Month > 1 || dateDifferent.Day > 1)
            {
                return String.Format("{0}:{1}", "The last active", lastActive.ToString("HH:mm dd/MM/yyyy"));
            }
            else if (dateDifferent.Hour > 0)
            {
                return String.Format("{0}: {1} {2}", "The last active", dateDifferent.ToString("hh"), "hours ago");
            }
            else if (dateDifferent.Minute > 10)
            {
                return String.Format("{0}: {1} {2}", "The last active", dateDifferent.ToString("mm"), "minutes ago");
            }
            else return "Online";
        }

        public string calculateStateLastMessage(DateTime lastMessage)
        {
            var lastMessageMilis = (Int32)(lastMessage.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds;
            var dateNowMillis = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds;

            var dateDifferent = UnixTimeStampToDateTime((Int32)(dateNowMillis - lastMessageMilis - 7200));

            if (dateDifferent.Year > 1970)
            {
                return lastMessage.ToString("dd.MM.yyyy");
            }
            else if (dateDifferent.Hour > 48)
            {
                return lastMessage.ToString("dd MMM", new CultureInfo("en-US"));
            }
            else if (dateDifferent.Hour > 24)
            {
                return "yesterday";
            }
            else return lastMessage.ToString("HH:mm");
        }

        public DateTime convertStringToDate(string inputDateString)
        {
            return DateTime.ParseExact(inputDateString, "yyyy-MM-dd'T'HH:mm:ss", null);
        }

        public DateTime convertStringToDateWithoutTime(string inputDateString)
        {
            return DateTime.ParseExact(inputDateString, "yyyy-MM-dd", null);
        }
    }
}