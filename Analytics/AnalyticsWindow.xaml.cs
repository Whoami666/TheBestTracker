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

namespace TheBestTracker.UserInterface
{
    /// <summary>
    /// Логика взаимодействия для AnalyticsWindow.xaml
    /// </summary>
    public partial class AnalyticsWindow : Window
    {
        private List<int> listId = new List<int>();

        string currentCategoryName;

        public AnalyticsWindow()
        {
            InitializeComponent();

            using (Context context = new Context())
            {
                categoryListBox.ItemsSource = context.Category.ToList();
                //colorsListBox.ItemsSource = context.Category.ToList();
            }
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
            allHoursTextBlock.Text = s;

        }

        private string GetString(int days)
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

        private void weekHoursTextBlock_Initialized(object sender, EventArgs e)
        {            
            TextBlock weekHoursTextBlock = sender as TextBlock;
            weekHoursTextBlock.Text = GetString(-7);
        }

        private void monthHoursTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock monthHoursTextBlock = sender as TextBlock;
            monthHoursTextBlock.Text = GetString(-30);
        }

        private void percentTextBlock_Initialized(object sender, EventArgs e)
        {
            int allHours = 0;
            int currentAllHours = 0;
            using (Context context = new Context())
            {
                foreach (var entry in context.CategoryTime)
                {
                    allHours++;
                    if (entry.CategoryName == currentCategoryName)
                    {
                        currentAllHours++;
                    }
                }
            }
            TextBlock percentTextBlock = sender as TextBlock;
            string s = string.Format("{0,-10:P1}", (double)currentAllHours / allHours);
            percentTextBlock.Text = s;
        }

        private void yearHoursTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock yearHoursTextBlock = sender as TextBlock;
            yearHoursTextBlock.Text = GetString(-365);
        }
    }
}
