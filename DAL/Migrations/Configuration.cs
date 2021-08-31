using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Migrations
{
    public class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = typeof(Context).FullName;
        }
    }
}
