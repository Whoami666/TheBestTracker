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

namespace TheBestTracker.UserInterface.SeeThe
{
    /// <summary>
    /// Логика взаимодействия для SeeTheMonth.xaml
    /// </summary>
    public partial class SeeTheMonth : Window
    {
        public SeeTheMonth()
        {
            InitializeComponent();
            datePicker.SelectedDate = DateTime.Today;

            GatherInfo();
        }

        public List<Category> thisMonday = new List<Category>();
        public List<Category> thisTuesday = new List<Category>();
        public List<Category> thisWednesday = new List<Category>();
        public List<Category> thisThursday = new List<Category>();
        public List<Category> thisFriday = new List<Category>();
        public List<Category> thisSaturday = new List<Category>();
        public List<Category> thisSunday = new List<Category>();

        public static CultureInfo cul = CultureInfo.CurrentCulture;
        public DateTime selectedDate = DateTime.Today;
        public int selectedMonth = cul.Calendar.GetMonth(DateTime.Today); //вытаскиваем номер месяца
        public int selectedYear = cul.Calendar.GetYear(DateTime.Today); //вытаскиваем номер года

        private List<int> listId = new List<int>();


        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDate = (DateTime)datePicker.SelectedDate;
            selectedMonth = cul.Calendar.GetMonth(selectedDate);
            selectedYear = cul.Calendar.GetYear(selectedDate);
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

                thisWednesday.Clear();
                thisTuesday.Clear();
                thisMonday.Clear();
                thisThursday.Clear();
                thisFriday.Clear();
                thisSaturday.Clear();
                thisSunday.Clear();

                for (int i = 0; i < 720; i++)
                {
                    thisMonday.Add(nothing);
                    thisTuesday.Add(nothing);
                    thisWednesday.Add(nothing);
                    thisThursday.Add(nothing);
                    thisFriday.Add(nothing);
                    thisSaturday.Add(nothing);
                    thisSunday.Add(nothing);
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
              //      DateTime weekAgo = DateTime.Now.Date.AddDays(-7);
               //     DateTime weekAhead = DateTime.Now.Date.AddDays(7);

                    if (cul.Calendar.GetMonth(category.Date) == selectedMonth && cul.Calendar.GetYear(category.Date) == selectedYear)
                    {
                        AddActivity(thisMonday, "Monday", category, allCategories, timeBlocksSorted);
                        AddActivity(thisTuesday, "Tuesday", category, allCategories, timeBlocksSorted);
                        AddActivity(thisWednesday, "Wednesday", category, allCategories, timeBlocksSorted);
                        AddActivity(thisThursday, "Thursday", category, allCategories, timeBlocksSorted);
                        AddActivity(thisFriday, "Friday", category, allCategories, timeBlocksSorted);
                        AddActivity(thisSaturday, "Saturday", category, allCategories, timeBlocksSorted);
                        AddActivity(thisSunday, "Sunday", category, allCategories, timeBlocksSorted);
                    }
                }
            }

            SetListBoxToNull();
            MondayCategoryListBox.ItemsSource = thisMonday;
            TuesdayCategoryListBox.ItemsSource = thisTuesday;
            WednesdayCategoryListBox.ItemsSource = thisWednesday;
            ThursdayCategoryListBox.ItemsSource = thisThursday;
            FridayCategoryListBox.ItemsSource = thisFriday;
            SaturdayCategoryListBox.ItemsSource = thisSaturday;
            SundayCategoryListBox.ItemsSource = thisSunday;
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

        static int GetWeekNumberOfMonth(DateTime date) //https://stackoverflow.com/questions/23060121/get-week-of-month-c-sharp
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            return (date - firstMonthMonday).Days / 7 + 1;
        }
    }
}
