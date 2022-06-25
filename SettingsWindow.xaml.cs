using System;
using System.Collections;
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
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private List<int> listId = new List<int>();
        
        public SettingsWindow()
        {
            InitializeComponent();
            //цвет по умолчанию
            colorPicker.SelectedColor = Colors.Red;
            foregroundColorPicker.SelectedColor = Colors.Black;
            //DefaultBlocks();
            //DefaultCategories(); // добавление дефолтных категорий в базу данных
            using (Context context = new Context())
            {
                categoryListBox.ItemsSource = context.Category.ToList();
                colorsListBox.ItemsSource = context.Category.ToList();
            }
        }

        private void CategoryTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock CategoryTextBlock = sender as TextBlock;
            Category category = CategoryTextBlock.DataContext as Category;
            CategoryTextBlock.Text = category.Name;
            listId.Add(category.Id);
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

        private void AddNewCategory(string name, int productive, string color, string foreground)
        {
            using (Context context = new Context())
            {
                var newCategory = new Category
                {
                    Name = name,
                    Productive = productive,
                    Color = color,
                    ForegroundColor = foreground
                };                
                context.Category.Add(newCategory);
                context.SaveChanges();
            }
        }

        private void RemoveOldCategory(int id)
        {
            
            using (Context context = new Context())
            {
                foreach(var entety in context.Category)
                {
                    if (entety.Id == id)
                    {
                        context.Category.Remove(entety);
                        break;
                    }

                }
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
                    Color = "Black",//System.Windows.Media.Color.FromArgb(255,255,255,255) //"Black"
                    ForegroundColor = "Yellow"
                };
                Category category2 = new Category
                {
                    Name = "Sleep",
                    Productive = 0,
                    Color = "Red",//System.Windows.Media.Color.FromArgb(255, 255, 0, 0)//"Red"
                    ForegroundColor = "Black"
                };
                Category category3 = new Category
                {
                    Name = "Eat",
                    Productive = 0,
                    Color = "White",//System.Windows.Media.Color.FromArgb(255, 0, 0, 0)//"White"
                    ForegroundColor = "Black"
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
                List<string> timeBlocks = new List<string>
                    { "0:00-1:00", "1:00-2:00","2:00-3:00", "3:00-4:00", "4:00-5:00", "5:00-6:00",  "6:00-7:00",
                      "7:00-8:00", "8:00-9:00",  "9:00-10:00", "10:00-11:00", "11:00-12:00",
                      "12:00-13:00",  "13:00-14:00", "14:00-15:00", "15:00-16:00", "16:00-17:00", "17:00-18:00",
                      "18:00-19:00", "19:00-20:00", "20:00-21:00", "21:00-22:00", "22:00-23:00", "23:00-24:00"
                    };
                for (int i = 0; i < 24; i++)
                {
                    TimeBlocks block = new TimeBlocks
                    {
                        Index = i,
                        Time = timeBlocks[i]
                    };
                    context.TimeBlock.Add(block);

                }

                context.SaveChanges(); //Чтобы добавленные объекты отправились в базу данных, нужно вызвать метод, сохраняющий изменения

                MessageBox.Show("Data saved.");
            }
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            if (PassCategoryName.Text.Length == 0 || OneInteger(PassProductive.Text) == false)
            {
                MessageBox.Show("Please check your data. Productive category must be 0, 1 or 2. Category name must be at least 1 character long");
                SettingsWindow userWindow = new SettingsWindow();
                userWindow.Show();
                this.Close();
            }

            else
            {
                string name;
                int productive;
                string color, foreground;

                productive = int.Parse(PassProductive.Text);
                name = PassCategoryName.Text;
                color = colorPicker.SelectedColorText;
                foreground = foregroundColorPicker.SelectedColorText;

                AddNewCategory(name, productive, color, foreground);
                MessageBox.Show("Ok");

                SettingsWindow userWindow = new SettingsWindow();
                userWindow.Show();
                this.Close();
            }
            this.Close();
        }

        private void RemoveCategory_Click(object sender, RoutedEventArgs e)
        {

            //categoryListBox.Items.Clear();
            if (categoryListBox.SelectedIndex != -1)
            {
                RemoveOldCategory(listId[categoryListBox.SelectedIndex]);
                MessageBox.Show("Ok");

                SettingsWindow userWindow = new SettingsWindow();
                userWindow.Show();
                this.Close();
            }
            else
                MessageBox.Show("Nothing is selected");
           
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            //MessageBox.Show(colorPicker.SelectedColorText);
            
            
        }
    }
}
