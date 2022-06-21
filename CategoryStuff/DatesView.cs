using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTracker.CategoryStuff.DataView
{
    public class DatesView
    {
        public string Name { get; set; }
        public List<DateName> Dates { get; set; }
        public DatesView(string name)
        {
            Name = name;
            Dates = new List<DateName>();
        }
    }

    public class DateName
    {
        public string Name { get; set; }
        public DateName(int name)
        {
            Name = name.ToString();
        }

    }

    
    public class WeeksView
    {
        public string Name { get; set; }
        public List<Category> DayCategory { get; set; }
        public WeeksView(string name)
        {
            Name = name;
            DayCategory = new List<Category>();
        }


    }
}
