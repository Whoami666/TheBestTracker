using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBestTracker.CategoryStuff;

namespace TheBestTracker
{
    class Context : DbContext
    {
        public DbSet<Category> Category { get; set; }

        public Context() : base("DBConnection")
        {

        }
    }
}
