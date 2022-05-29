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
                CategoryListBox.ItemsSource = context.Category.ToList();
                TimeListBox.ItemsSource = context.TimeBlock.ToList();
            }
        }
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
