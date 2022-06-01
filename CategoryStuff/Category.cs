using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TheBestTracker.CategoryStuff
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Productive { get; set; }

        public string Color { get; set; }
        public Category()
        {

        }
    }
}
