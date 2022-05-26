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
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()            //CategoryListBox.ItemsSource = new List<Category>
            //{
            //    new Category
            //    {
            //        Name = "Leonardo DiCaptio",
            //        Productive = 1
            //    },

            //    new Category
            //    {
            //        Name = "Not Leonardo",
            //        Productive = 0
            //    }
            //};
        {
            InitializeComponent();

        }

        private void Button_ClickPass(object sender, RoutedEventArgs e)
        {
            string password = "Pass";
            string login = "Admin";

            if (password == loginInput.Text & login == passInput.Text)
            {
                MessageBox.Show("Ok");
          //      AdminChoice1 adminWindow = new AdminChoice1();
            //    adminWindow.Show();
                this.Close();
            }

            else MessageBox.Show("Try again(((");
        }

        //private void CategoryTextBlock_Initialized(object sender, EventArgs e)
        //{
        //    TextBlock CategoryTextBlock = sender as TextBlock;
        //    Category category = CategoryTextBlock.DataContext as Category;
        //    CategoryTextBlock.Text = category.Name;
        //}

        //private void ProductiveTextBlock_Initialized(object sender, EventArgs e)
        //{
        //    TextBlock ProductiveTextBlock = sender as TextBlock;
        //    Category category = ProductiveTextBlock.DataContext as Category;
        //    ProductiveTextBlock.Text = category.Productive.ToString();
        //}
        //   private void Button_ClickPass(object sender, RoutedEventArgs e)
        //  {

        //      this.Close();
        //  }
    }
}
