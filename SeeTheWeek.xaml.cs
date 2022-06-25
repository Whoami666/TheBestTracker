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
    /// Логика взаимодействия для SeeTheWeek.xaml
    /// </summary>
    public partial class SeeTheWeek : Window
    {
        public SeeTheWeek()
        {
            InitializeComponent();

            using (Context context = new Context())
            {
                List<Category> allCategories = context.Category.ToList();
                TimeListBox.ItemsSource = context.TimeBlock.ToList();


                var categoryTimes = context.CategoryTime.ToList();
                List<Category> thisWeek = new List<Category>();

                foreach (var category in categoryTimes)
                {
                    DateTime weekAgo = DateTime.Now.Date.AddDays(-1);
                    if (category.Date.Date > weekAgo)
                    {
                        thisWeek.Add(allCategories.FirstOrDefault(c => c.Name == category.CategoryName));
                    } 
                }
                //    var week = categoryTimes.( => p.Age > 35);
                CategoryListBox.ItemsSource = thisWeek;
            }


        }

        //private void TicketsTextBlock_Initialized(object sender, EventArgs e)
        //{

        //    TextBlock TicketsTextBlock = sender as TextBlock;
        //    Hall hall = TicketsTextBlock.DataContext as Hall;
        //    TicketsTextBlock.Text = hall.Name;

        //}

        // TicketsListBox.ItemsSource = hallsShow;
        //    using (Context context = new Context())
        //    {
        //        Monday.ItemsSource = context.Category.Where(s => s.Productive == 1).ToList();
        //    }

        //    for (int i = 0; i < Monday.Items.Count; i++)
        //    {
        //        if (Monday.Items[i].Content.Text = "Sample")
        //        {
        //            li.ForeColor = Color.Green;
        //        }
        //    }

        //    ListViewItem li = new ListViewItem();
        //    li.ForeColor = Color.Red;
        //    li.Text = "Sample";
        //    listView1.Items.Add(li);
        //}

        //private void listView1_Refresh()
        //{
        //    for (int i = 0; i < Monday.Items.Count; i++)
        //    {
        //        Monday.Items[i].Foreground = Color.Red;
        //        for (int j = 0; j < existingStudents.Count; j++)
        //        {
        //            if (listView1.Items[i].ToString().Contains(existingStudents[j]))
        //            {
        //                listView1.Items[i].BackColor = Color.Green;
        //            }
        //        }
        //    }
        //}



    }
}
