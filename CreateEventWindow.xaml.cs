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
using TheBestTracker.UserInterface.SeeThe;

namespace TheBestTracker.UserInterface
{
    /// <summary>
    /// Логика взаимодействия для CreateEventWindow.xaml
    /// </summary>
    public partial class CreateEventWindow : Window
    {
        private List<int> listId = new List<int>();

        private bool isCategoryTime = false;

        private TimeBlocks timeBlock = new TimeBlocks();

        private Category category;

        static private CategoryTime categoryTime = new CategoryTime();
      

        public CreateEventWindow()
        {
            InitializeComponent();
            datePicker.SelectedDate = DateTime.Today;

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

        //private void dateTimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    categoryTime.Date = (DateTime)dateTimePicker.Value;
        //    //MessageBox.Show(categoryTime.Date.ToString());
        //    isCategoryTime = true;
        //}

        //private void integerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    //durationOfEventInMinutes = (int)integerUpDown.Value;
        //    //MessageBox.Show(durationOfEventInMinutes.ToString());
        //    isDuration = true;
        //}

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryTime.Date = (DateTime)datePicker.SelectedDate;
            //MessageBox.Show(categoryTime.Date.ToString());
            isCategoryTime = true;
        }

        private void addEvent_Click(object sender, RoutedEventArgs e)
        {
            if (!isCategoryTime || (categoryListBox.SelectedIndex == -1) || (startTimeBlockListBox.SelectedIndex == -1))
            {
                MessageBox.Show("Please check your data. You must choose something in all fields.");
                return;
            }
            else
            {
                using (Context context = new Context())
                {
                    foreach (var entry in context.Category)
                    {
                        if (entry.Id == listId[categoryListBox.SelectedIndex])
                        {
                            category = entry;                          
                            break;
                        }
                    }
                    categoryTime.CategoryName = category.Name;

                    if (startTimeBlockListBox.SelectedIndex >= endTimeBlockListBox.SelectedIndex)
                    {
                        categoryTime.TimeBlock = ((ListBoxItem)startTimeBlockListBox.SelectedItem).Content.ToString();
                        context.CategoryTime.Add(categoryTime);
                        MessageBox.Show(category.Name  + "   " + categoryTime.Date + "   " + categoryTime.TimeBlock);
                        context.SaveChanges();
                    }
                    else
                    {
                        for (int index = startTimeBlockListBox.SelectedIndex; index <= endTimeBlockListBox.SelectedIndex; index++)
                        {
                            foreach (var entry in context.TimeBlock)
                            {
                                if (entry.Index == index)
                                {
                                    categoryTime.TimeBlock = entry.Time;
                                    context.CategoryTime.Add(categoryTime);
                            //        MessageBox.Show(category.Name +  "   " + categoryTime.Date + "   " + categoryTime.TimeBlock);
                                    break;
                                }                                
                            }
                            context.SaveChanges();
                        }
                    }                                   
                }
            }
          //  this.Close();
        }

        private void seeTheWeekClick(object sender, RoutedEventArgs e)
        {
            SeeTheWeekV2 seeTheWeek = new SeeTheWeekV2();
            seeTheWeek.Show();
            this.Close();
        }
    }
}
