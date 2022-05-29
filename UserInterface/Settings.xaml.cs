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

        public Settings()
        {
            InitializeComponent();
         //   DefaultBlocks();
           // DefaultCategories(); // добавление дефолтных категорий в базу данных
            using (Context context = new Context())
            {
                CategoryListBox.ItemsSource = context.Category.ToList();
            }
        }


        private void CategoryTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock CategoryTextBlock = sender as TextBlock;
            Category category = CategoryTextBlock.DataContext as Category;
            CategoryTextBlock.Text = category.Name;
        }

        private void ProductiveTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock ProductiveTextBlock = sender as TextBlock;
            Category category = ProductiveTextBlock.DataContext as Category;
            ProductiveTextBlock.Text = category.Productive.ToString();
        }

        protected static bool OneInteger(string str)
        {
            string[] integerlist = str.Split(new char[] { ' ', '-' });
            if (integerlist.Length != 1) return false;
            else
            {
                if (int.TryParse(integerlist[0], out int x))
                {
                    if (x == 0 || x == 1 || x == 2) return true;
                    else return false;
                }
                else return false;
            }
        }

        private void Button_ClickPass(object sender, RoutedEventArgs e)
        {

            if (PassCategoryName.Text.Length == 0 || OneInteger(PassProductive.Text) == false)
            {
                MessageBox.Show("Please check your data. Productive category must be 0, 1 or 2. Category name must be at least 1 character long");
                Settings userWindow = new Settings();
                userWindow.Show();
                this.Close();
            }

            else
            {
                string name;
                int productive;
                productive = int.Parse(PassProductive.Text);
                name = PassCategoryName.Text;
                AddNewCategory(name, productive);
                MessageBox.Show("Ok");

                Settings userWindow = new Settings();
                userWindow.Show();
                this.Close();
            }
            this.Close();
        }

        private void AddNewCategory(string name, int productive)
        {
            using (Context context = new Context())
            {
                var newCategory = new Category
                {
                    Name = name,
                    Productive = productive
                };
                context.Category.Add(newCategory);
                context.SaveChanges();
            }
        }


        private void DefaultCategories()
        {
            using (Context context = new Context()) //Создание подключения (локальной копии ДБ)
            {
                //Объявление объектов
                Category category1 = new Category
                {
                    Name = "Lana Del Rey",
                    Productive = 1,
                    Color = "Black"
                };
                Category category2 = new Category
                {
                    Name = "Sleep",
                    Productive = 0,
                    Color = "Red"
                };
                Category category3 = new Category
                {
                    Name = "Eat",
                    Productive = 0,
                    Color = "White"
                };

                context.Category.Add(category1);
                context.Category.Add(category2);
                context.Category.Add(category3);

                context.SaveChanges(); //Чтобы добавленные объекты отправились в базу данных, нужно вызвать метод, сохраняющий изменения

                MessageBox.Show("Data saved.");
            }
        }

        private void DefaultBlocks()
        {
            using (Context context = new Context()) //Создание подключения (локальной копии ДБ)
            {
                //Объявление объектов
                int prevTime = 0;
                for (int i = 0; i < 24; i++)
                {
                    string timer;
                    prevTime = prevTime + 1;
                    if (prevTime < 10)
                    {
                        timer = "0" + prevTime.ToString() + ":00";
                    }
                    else timer = prevTime.ToString() + ":00";
                    TimeBlocks block = new TimeBlocks
                    {
                        Index = i,
                        Time = timer
                    };
                    context.TimeBlock.Add(block);
                }

                context.SaveChanges(); //Чтобы добавленные объекты отправились в базу данных, нужно вызвать метод, сохраняющий изменения

                MessageBox.Show("Data saved.");
            }
        }
    }
}
