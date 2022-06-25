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

namespace TheBestTracker.UserInterface
{
    /// <summary>
    /// Логика взаимодействия для SeeTheWeekV2.xaml
    /// </summary>
    public partial class SeeTheWeekV2 : Window
    {
        public List<DatesView> getDates = new List<DatesView>()
        {
            new DatesView("Monday"),
            new DatesView("Tuesday"),
            new DatesView("Wednesday"),
            new DatesView("Thursday"),
            new DatesView("Friday"),
            new DatesView("Saturday"),
            new DatesView("Sunday")
        };

        Dictionary<string, List<Category>> calendarWeek = new Dictionary<string, List<Category>>();

        private void Fill()
        {
            foreach (var week in getDates)
            {
                calendarWeek.Add(week.Name, new List<Category>());
            }
        }



        public static CultureInfo cul = CultureInfo.CurrentCulture;
        public DateTime selectedDate = DateTime.Today;
        public int selectedWeek = cul.Calendar.GetWeekOfYear(DateTime.Today, CalendarWeekRule.FirstDay, DayOfWeek.Monday); //вытаскиваем номер недели
        public int selectedYear = cul.Calendar.GetYear(DateTime.Today); //вытаскиваем номер года

        private List<int> listId = new List<int>();
        public SeeTheWeekV2()
        {
            Fill();
            InitializeComponent();
            datePicker.SelectedDate = DateTime.Today;

            GatherInfo();
        }


        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDate = (DateTime)datePicker.SelectedDate;
            selectedWeek = cul.Calendar.GetWeekOfYear(selectedDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            GatherInfo();

            //      SeeTheWeekV2 thisWindow = new SeeTheWeekV2();
            //    thisWindow.Visibility = Visibility.Visible;
            //   this.Visibility = Visibility.Collapsed;
        }


        public void GatherInfo()
        {
            using (Context context = new Context())
            {
                Category nothing = context.Category.ToList().FirstOrDefault(c => c.Name == "Nothing");

                foreach (var list in calendarWeek)
                {
                    list.Value.Clear();
                }

                for (int i = 0; i < 24; i++)
                {
                    foreach (var list in calendarWeek)
                    {
                        list.Value.Add(nothing);
                    }
                }

                List<string> timeBlocksSorted = new List<string>();
                timeBlocksSorted.Clear();
                var timeBlocks = context.TimeBlock.ToList();
                foreach (var item in timeBlocks)
                {
                    timeBlocksSorted.Add(item.Time);
                }


                List<Category> allCategories = context.Category.ToList();
                TimeListBox.ItemsSource = context.TimeBlock.ToList();
                var categoryTimes = context.CategoryTime.ToList();

                foreach (var category in categoryTimes)
                {
                    DateTime weekAgo = DateTime.Now.Date.AddDays(-7);
                    DateTime weekAhead = DateTime.Now.Date.AddDays(7);

                    if (cul.Calendar.GetWeekOfYear(category.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday) == selectedWeek)
                    {
                        foreach (var list in calendarWeek)
                        {
                            AddActivity(list.Value, list.Key, category, allCategories, timeBlocksSorted);
                        }
                    }
                }
            }

            SetListBoxToNull();

            MondayCategoryListBox.ItemsSource = calendarWeek["Monday"];
            TuesdayCategoryListBox.ItemsSource = calendarWeek["Tuesday"];
            WednesdayCategoryListBox.ItemsSource = calendarWeek["Wednesday"];
            ThursdayCategoryListBox.ItemsSource = calendarWeek["Thursday"];
            FridayCategoryListBox.ItemsSource = calendarWeek["Friday"];
            SaturdayCategoryListBox.ItemsSource = calendarWeek["Saturday"];
            SundayCategoryListBox.ItemsSource = calendarWeek["Sunday"];
        }

        public void SetListBoxToNull()
        {
            MondayCategoryListBox.ItemsSource = null;
            TuesdayCategoryListBox.ItemsSource = null;
            WednesdayCategoryListBox.ItemsSource = null;
            ThursdayCategoryListBox.ItemsSource = null;
            FridayCategoryListBox.ItemsSource = null;
            SaturdayCategoryListBox.ItemsSource = null;
            SundayCategoryListBox.ItemsSource = null;
        }

        public List<Category> AddActivity(List<Category> dayList, string day, CategoryTime category, List<Category> allCategories, List<string> timeBlocks)
        {
            if (category.Date.Date.DayOfWeek.ToString() == day)
            {
                int number = timeBlocks.IndexOf(category.TimeBlock);
                if (number > -1) dayList[number] = allCategories.FirstOrDefault(c => c.Name == category.CategoryName);
            }
            return dayList;
        }

        private void CategoryTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock CategoryTextBlock = sender as TextBlock;
            Category category = CategoryTextBlock.DataContext as Category;
            CategoryTextBlock.Text = category.Name;
            CategoryTextBlock.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(category.Color);
            CategoryTextBlock.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(category.ForegroundColor);
            listId.Add(category.Id);
        }

        private void addEventClick(object sender, RoutedEventArgs e)
        {
            CreateEventWindow сreateEventWindow = new CreateEventWindow();
            сreateEventWindow.Show();
            this.Close();
        }

        private void showAnalytics_Click(object sender, RoutedEventArgs e)
        {
            AnalyticsWindow analyticsWindow = new AnalyticsWindow();
            analyticsWindow.Show();
            this.Close();
        }
    }
}
