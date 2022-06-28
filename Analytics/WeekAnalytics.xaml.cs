using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TheBestTracker.CategoryStuff;
using TheBestTracker.CategoryStuff.DataView;
using TheBestTracker.UserInterface.MessageBoxes;

namespace TheBestTracker.UserInterface.Analytics
{
    /// <summary>
    /// Логика взаимодействия для WeekAnalytics.xaml
    /// </summary>
    public partial class WeekAnalytics : Window
    {
        private List<int> listId = new List<int>();
        string productiveStats, deltaStats, currentStats, currentCategoryName, allStats;
        string chosenPeriod;

        public static CultureInfo cul = CultureInfo.CurrentCulture;
        public DateTime selectedDate;
        public int selectedWeek = cul.Calendar.GetWeekOfYear(DateTime.Today, CalendarWeekRule.FirstDay, DayOfWeek.Monday); //вытаскиваем номер недели
        public int selectedYear = cul.Calendar.GetYear(DateTime.Today); //вытаскиваем номер года


        public WeekAnalytics(string period, DateTime date)
        {
            chosenPeriod = period;
            selectedDate = date;


            InitializeComponent();
          //  datePicker.Text = selectedDate.ToString();

            using (Context context = new Context())
            {
                categoryListBox.ItemsSource = context.Category.ToList();
            }
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDate = (DateTime)datePicker.SelectedDate;
            WeekAnalytics analyticsWindow = new WeekAnalytics(chosenPeriod, selectedDate);
            analyticsWindow.Show();
            this.Close();
        }

        public Dictionary<string, string> GatherInfo(string categoryName)
        {
            using (Context context = new Context())
            {
                DateTime date = selectedDate.Date;
                var now = DateTime.Now.Date;
                if (chosenPeriod == "Day")
                {
                    currentStats = WeekMonthYearAnalytics.GetStatsSelectedPeriod(date, date, categoryName);
                    productiveStats = WeekMonthYearAnalytics.GetProdictivityPeriod(date, date, categoryName);
                    allStats = WeekMonthYearAnalytics.AllHoursPeriod(date, date);
                    deltaStats = WeekMonthYearAnalytics.GetDeltaPeriod(date, date, categoryName);
                }
                else if (chosenPeriod == "Week")
                {
                    var startPeriod = date.AddDays(-((date.DayOfWeek - System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek + 7) % 7)).Date;
                    currentStats = WeekMonthYearAnalytics.GetStatsSelectedPeriod(startPeriod, startPeriod.AddDays(6), categoryName);
                    productiveStats = WeekMonthYearAnalytics.GetProdictivityPeriod(startPeriod, now, categoryName);
                    allStats = WeekMonthYearAnalytics.AllHoursPeriod(startPeriod, now);
                    deltaStats = WeekMonthYearAnalytics.GetDeltaPeriod(startPeriod, now, categoryName);
                }
                else if (chosenPeriod == "Month")
                {
                    var lastDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month);
                    DateTime startPeriod = new DateTime(date.Year, date.Month, 1).Date;
                    DateTime endPeriod = new DateTime(date.Year, date.Month, lastDayOfMonth).Date;

                    if (now.Month == selectedDate.Month)
                    {
                        currentStats = WeekMonthYearAnalytics.GetStatsSelectedPeriod(startPeriod, now, categoryName);
                        productiveStats = WeekMonthYearAnalytics.GetProdictivityPeriod(startPeriod, now, categoryName);
                        allStats = WeekMonthYearAnalytics.AllHoursPeriod(startPeriod, now);
                        deltaStats = WeekMonthYearAnalytics.GetDeltaPeriod(startPeriod, now, categoryName);
                    }
                    else
                    {
                        currentStats = WeekMonthYearAnalytics.GetStatsSelectedPeriod(startPeriod, endPeriod, categoryName);
                        productiveStats = WeekMonthYearAnalytics.GetProdictivityPeriod(startPeriod, endPeriod, categoryName);
                        allStats = WeekMonthYearAnalytics.AllHoursPeriod(startPeriod, endPeriod);
                        deltaStats = WeekMonthYearAnalytics.GetDeltaPeriod(startPeriod, endPeriod, categoryName);
                    }
                }
                else if (chosenPeriod == "Year")
                {
                    DateTime startPeriod = new DateTime(date.Year, 1, 1).Date;
                    DateTime endPeriod = new DateTime(date.Year, 12, 31).Date;

                    if (now.Year == selectedDate.Year)
                    {
                        currentStats = WeekMonthYearAnalytics.GetStatsSelectedPeriod(startPeriod, now, categoryName);
                        productiveStats = WeekMonthYearAnalytics.GetProdictivityPeriod(startPeriod, now, categoryName);
                        allStats = WeekMonthYearAnalytics.AllHoursPeriod(startPeriod, now);
                        deltaStats = WeekMonthYearAnalytics.GetDeltaPeriod(startPeriod, now, categoryName);
                    }
                    else
                    {
                        currentStats = WeekMonthYearAnalytics.GetStatsSelectedPeriod(startPeriod, endPeriod, categoryName);
                        productiveStats = WeekMonthYearAnalytics.GetProdictivityPeriod(startPeriod, endPeriod, categoryName);
                        allStats = WeekMonthYearAnalytics.AllHoursPeriod(startPeriod, endPeriod);
                        deltaStats = WeekMonthYearAnalytics.GetDeltaPeriod(startPeriod, endPeriod, categoryName);
                    }
                }
               

            }

            Dictionary<string, string> textBox = new Dictionary<string, string>();
            textBox.Add("currentStats", currentStats);
            textBox.Add("productiveStats", productiveStats);
            textBox.Add("allStats", allStats);
            textBox.Add("deltaStats", deltaStats);
            return textBox;
        }


        private void CategoryTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock CategoryTextBlock = sender as TextBlock;
            Category category = CategoryTextBlock.DataContext as Category;
            CategoryTextBlock.Text = category.Name;
            currentCategoryName = category.Name;
            CategoryTextBlock.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(category.Color);
            CategoryTextBlock.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(category.ForegroundColor);
            listId.Add(category.Id);
        }

        private void ProductiveTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock ProductiveTextBlock = sender as TextBlock;
            Category category = ProductiveTextBlock.DataContext as Category;
            ProductiveTextBlock.Text = category.Productive.ToString();
        }

        private void allHoursTextBlock_Initialized(object sender, EventArgs e)
        {
            int currentAllHours = 0;
            using (Context context = new Context())
            {
                foreach (var entry in context.CategoryTime)
                {
                    if (entry.CategoryName == currentCategoryName)
                    {
                        currentAllHours++;
                    }

                }
            }
            TextBlock allHoursTextBlock = sender as TextBlock;
            string s = string.Format("{0,-10}", currentAllHours);
            //Category category = allHoursTextBlock.DataContext as Category;
            allHoursTextBlock.Text = fiveDigits(s);

        }

        private void periodHoursTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock weekHoursTextBlock = sender as TextBlock;
            Category category = weekHoursTextBlock.DataContext as Category;
            weekHoursTextBlock.Text = GatherInfo(currentCategoryName)["currentStats"];
        }

        private void deltaTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock weekHoursTextBlock = sender as TextBlock;
            weekHoursTextBlock.Text = GatherInfo(currentCategoryName)["deltaStats"];
        }

        private void percentTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock percentTextBlock = sender as TextBlock;
            string s = string.Format("{0,-10:P1}", double.Parse(GatherInfo(currentCategoryName)["currentStats"])/ int.Parse(GatherInfo(currentCategoryName)["allStats"]));
            percentTextBlock.Text = fiveDigits(s);
        }

        private void prodTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock yearHoursTextBlock = sender as TextBlock;
            yearHoursTextBlock.Text = GatherInfo(currentCategoryName)["productiveStats"];
        }


        private string fiveDigits(string str)
        {
            if (str.Length == 1)
            {
                return "  " + str + "  ";
            }
            else if (str.Length == 3)
            {
                return "  " + str + "  ";
            }
            else if (str.Length == 4)
            {
                return str + "  ";
            }
            else if (str.Length == 2)
            {
                return " " + str + "  ";
            }
            else return str;
        }

        private void addEventClick(object sender, RoutedEventArgs e)
        {
            CreateEventWindow сreateEventWindow = new CreateEventWindow(null);
            сreateEventWindow.Show();
            this.Close();
        }

        private void showAnalytics_Click(object sender, RoutedEventArgs e)
        {
            AnalyticsWindow analyticsWindow = new AnalyticsWindow(null);
            analyticsWindow.Show();
            this.Close();
        }

        private void settingsClick(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }

        private void homeClick(object sender, RoutedEventArgs e)
        {
            SeeTheWeekV2 see = new SeeTheWeekV2();
            see.Show();
            this.Close();
        }

        private void yearClickInitialized(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (chosenPeriod == "Year")
            {
                button.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#32373c");
                button.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#00bbd3");
            }          
        }

        private void dayClickInitialized(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (chosenPeriod == "Day")
            {
                button.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#32373c");
                button.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#00bbd3");
            }
        }

        private void monthClickInitialized(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (chosenPeriod == "Month")
            {
                button.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#32373c");
                button.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#00bbd3");
            }
        }

        private void weekClickInitialized(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (chosenPeriod == "Week")
            {
                button.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#32373c");
                button.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#00bbd3");
            }
        }

        private void exitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void showWeekClick(object sender, RoutedEventArgs e)
        {
            WeekAnalytics see = new WeekAnalytics("Week", DateTime.Now);
            see.Show();
            this.Close();
        }

        private void showMonthClick(object sender, RoutedEventArgs e)
        {
            WeekAnalytics see = new WeekAnalytics("Month", DateTime.Now);
            see.Show();
            this.Close();
        }

        private void showDayClick(object sender, RoutedEventArgs e)
        {
            WeekAnalytics see = new WeekAnalytics("Day", DateTime.Now);
            see.Show();
            this.Close();
        }

        private void showYearClick(object sender, RoutedEventArgs e)
        {
            WeekAnalytics see = new WeekAnalytics("Year", DateTime.Now);
            see.Show();
            this.Close();
        }
    }
}
