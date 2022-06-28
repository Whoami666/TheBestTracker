using System;
using System.Collections.Generic;
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
using TheBestTracker.UserInterface.Analytics;
using TheBestTracker.UserInterface.Analytics.Charts;
using TheBestTracker.UserInterface.MessageBoxes;

namespace TheBestTracker.UserInterface
{
    /// <summary>
    /// Логика взаимодействия для AnalyticsWindow.xaml
    /// </summary>
    public partial class AnalyticsWindow : Window
    {
        private List<int> listId = new List<int>();
        public int nDays;
        string currentCategoryName;

        public AnalyticsWindow(int? number)
        {
            if (number == null)
            {
                nDays = -7;
            }
            else
            {
                nDays = -(int)number;
            }
            
            InitializeComponent();

            using (Context context = new Context())
            {
                categoryListBox.ItemsSource = context.Category.ToList();
                //colorsListBox.ItemsSource = context.Category.ToList();
            }
        }

        public static List<KeyValuePair<string, int>> DataCharts()
        {
            List<KeyValuePair<string, int>> sourceList = new List<KeyValuePair<string, int>>();
            int currentCategoryHours = 0;
            using (Context context = new Context())
            {
                foreach (var entryCategory in context.Category)
                {
                    currentCategoryHours = 0;
                    foreach (var entryCategoryTime in context.CategoryTime)
                    {
                        if (entryCategoryTime.CategoryName == entryCategory.Name)
                        {
                            currentCategoryHours++;
                        }
                    }
                    sourceList.Add(new KeyValuePair<string, int>(entryCategory.Name, currentCategoryHours));
                }
            }
            return sourceList;
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
            weekHoursTextBlock.Text = fiveDigits(WeekMonthYearAnalytics.GetString(nDays, currentCategoryName));
        }

        private void deltaTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock weekHoursTextBlock = sender as TextBlock;
            weekHoursTextBlock.Text = WeekMonthYearAnalytics.GetDelta(nDays, currentCategoryName);
        }

        private void percentTextBlock_Initialized(object sender, EventArgs e)
        {
            int allHours = WeekMonthYearAnalytics.AllHours(nDays);
            int currentAllHours = int.Parse(WeekMonthYearAnalytics.GetString(nDays, currentCategoryName));

            TextBlock percentTextBlock = sender as TextBlock;
            string s = string.Format("{0,-10:P1}", (double)currentAllHours/ allHours);
            percentTextBlock.Text = fiveDigits(s);
        }

        private void prodTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock yearHoursTextBlock = sender as TextBlock;
            yearHoursTextBlock.Text = WeekMonthYearAnalytics.GetProdictivity(nDays, currentCategoryName);
            
        }

        private void passAmountInitialized(object sender, EventArgs e)
        {
            TextBox passAmount = sender as TextBox;
            passAmount.Text = (-nDays).ToString();
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


        private void okClick(object sender, RoutedEventArgs e)
        {
            string number = PassAmount.Text;
            if (number.Length != 0)
            {
                if(WeekMonthYearAnalytics.OneInteger(number) == true)
                {
                    AnalyticsWindow analyticsWindow = new AnalyticsWindow(int.Parse(PassAmount.Text));
                    analyticsWindow.Show();
                    this.Close();
                }
                else
                {
                    NeedInteger Message = new NeedInteger();
                    Message.Show();
                }
            }
        }


        private void addEventClick(object sender, RoutedEventArgs e)
        {
            CreateEventWindow сreateEventWindow = new CreateEventWindow(null);
            сreateEventWindow.Show();
            this.Close();
        }

        private void showAnalytics_Click(object sender, RoutedEventArgs e)
        {
            AnalyticsWindow analyticsWindow = new AnalyticsWindow(nDays);
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

        private void pieChartClick(object sender, RoutedEventArgs e)
        {
            PieChartWindow pieChartWindow = new PieChartWindow();
            pieChartWindow.Show();
            this.Close();
        }

        private void columnChartClick(object sender, RoutedEventArgs e)
        {
            ColumnChartWindow columnChartWindow = new ColumnChartWindow();
            columnChartWindow.Show();
            this.Close();
        }
    }
}
