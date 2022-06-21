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

            //   CreateEventWindow сreateEventWindow = new CreateEventWindow();
            //   SettingsWindow сreateEventWindow = new SettingsWindow();
          //  SeeTheCalendar сreateEventWindow = new SeeTheCalendar();
          //  SeeTheMonthV3 сreateEventWindow = new SeeTheMonthV3();
            SeeTheWeekV2 сreateEventWindow = new SeeTheWeekV2();
            сreateEventWindow.Show();
            //  SeeTheWeek сreateEventWindow = new SeeTheWeek();
            //  сreateEventWindow.Show();
            this.Close();
        }


    }
}
