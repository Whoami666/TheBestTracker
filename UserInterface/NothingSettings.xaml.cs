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
    /// Логика взаимодействия для NothingSettings.xaml
    /// </summary>
    public partial class NothingSettings : Window
    {

            private List<int> listId = new List<int>();

            public NothingSettings()
            {
                InitializeComponent();

                colorPicker.SelectedColor = Colors.Transparent; //цвет по умолчанию


            }

            private void AddNewCategory(string name, int productive, string color, string foreground)
            {
                using (Context context = new Context())
                {
                    foreach (var entety in context.Category)
                    {
                        if (entety.Name == "Nothing")
                        {
                            context.Category.Remove(entety);
                            break;
                        }
                    }

                    context.SaveChanges();

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


            private void AddCategory_Click(object sender, RoutedEventArgs e)
            {
                string color;

                color = colorPicker.SelectedColorText;

                AddNewCategory("Nothing", 0, color, color);
                MessageBox.Show("Ok");

                SettingsWindow userWindow = new SettingsWindow();
                userWindow.Show();
                this.Close();
            }



            private void Exit_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
            }

            private void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
            {
                //MessageBox.Show(colorPicker.SelectedColorText);
            }

            private string GetColor()
            {
                using (Context context = new Context())
                {
                    foreach (var entety in context.Category)
                    {
                        if (entety.Name == "Nothing")
                        {
                            return entety.Color;
                        }
                    }
                    return null;
                }
            }

            private void ColorsTBInitialized(object sender, EventArgs e)
            {
                TextBlock ColorsTextBlock = sender as TextBlock;
                ColorsTextBlock.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(GetColor());
                ColorsTextBlock.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(GetColor());
            }
        }
}
