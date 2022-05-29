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
    public partial class CopySet : Window
    {

        public CopySet()
        {
            InitializeComponent();
            //    DefaultCategories(); добавление дефолтных категорий в базу данных
            using (Context context = new Context())
            {
                CategoryListBox.ItemsSource = context.Category.ToList();
            }
        }




    }
}
