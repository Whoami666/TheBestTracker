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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheBestTracker.CategoryStuff;
using TheBestTracker.UserInterface;
using TheBestTracker.UserInterface.SeeThe;

namespace TheBestTracker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeData();

            SeeTheWeekV2 сreateEventWindow = new SeeTheWeekV2();
            сreateEventWindow.Show();
            this.Close();
        }

        private void InitializeData()
        {
            DefaultCategories();
            DefaultBlocks();
        }

        private void DefaultCategories()
        {
            using (Context context = new Context()) //Создание подключения (локальной копии ДБ)
            {
                if (!context.Category.Any())
                {
                    Category category1 = new Category
                    {
                        Name = "Sleep",
                        Productive = 0,
                        Color = "Red",//System.Windows.Media.Color.FromArgb(255, 255, 0, 0)//"Red"
                        ForegroundColor = "Black"
                    };
                    Category category2 = new Category
                    {
                        Name = "Eat",
                        Productive = 0,
                        Color = "White",//System.Windows.Media.Color.FromArgb(255, 0, 0, 0)//"White"
                        ForegroundColor = "Black"
                    };
                    Category category3 = new Category
                    {
                        Name = "Nothing",
                        Productive = 0,
                        Color = "Transparent",
                        ForegroundColor = "Transparent"
                    };

                    context.Category.Add(category1);
                    context.Category.Add(category2);
                    context.Category.Add(category3);

                    context.SaveChanges(); //Чтобы добавленные объекты отправились в базу данных, нужно вызвать метод, сохраняющий изменения
                }
            }
        }

        private void DefaultBlocks()
        {
            using (Context context = new Context()) //Создание подключения (локальной копии ДБ)
            {
                if (!context.TimeBlock.Any())
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

                }
            }
        }


    }
}
