using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTracker.CategoryStuff.DataView
{
    public class WeekMonthYearAnalytics
    {
        public static string GetString(int days, string currentCategoryName)
        {
            int currentHours = 0;
            DateTime weekAgo = DateTime.Now.Date.AddDays(days);
            using (Context context = new Context())
            {
                foreach (var entry in context.CategoryTime)
                {
                    if (entry.CategoryName == currentCategoryName && entry.Date.Date > weekAgo)
                    {
                        currentHours++;
                    }
                }
            }
            string s = string.Format("{0,-10}", currentHours);
            return s;
        }

        public static string GetStatsSelectedWeek(string currentCategoryName, DateTime date)
        {
            int currentHours = 0;
            CultureInfo cul = CultureInfo.CurrentCulture;

            int selectedWeek = cul.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday); //вытаскиваем номер недели
            int selectedYear = cul.Calendar.GetYear(date); //вытаскиваем номер года

            using (Context context = new Context())
            {
                foreach (var entry in context.CategoryTime)
                {
                    int blockWeek = cul.Calendar.GetWeekOfYear(entry.Date.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                    int blockYear = cul.Calendar.GetYear(entry.Date.Date);
                    if (entry.CategoryName == currentCategoryName && blockYear == selectedYear && blockWeek == selectedWeek)
                    {
                        currentHours++;
                    }
                }
            }
            string s = string.Format("{0,-10}", currentHours);
            return s;
        }

        public static string GetStatsSelectedMonth(string currentCategoryName, DateTime date)
        {
            int currentHours = 0;
            CultureInfo cul = CultureInfo.CurrentCulture;

            int selectedYear = cul.Calendar.GetYear(date); //вытаскиваем номер года
            int selectedMonth = cul.Calendar.GetMonth(date); //вытаскиваем номер месяца

            using (Context context = new Context())
            {
                foreach (var entry in context.CategoryTime)
                {
                    int blockMonth = cul.Calendar.GetMonth(entry.Date.Date);
                    int blockYear = cul.Calendar.GetYear(entry.Date.Date);
                    if (entry.CategoryName == currentCategoryName && blockYear == selectedYear && blockMonth == selectedMonth)
                    {
                        currentHours++;
                    }
                }
            }
            string s = string.Format("{0,-10}", currentHours);
            return s;
        }



    }
}
