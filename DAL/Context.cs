using DAL.Configurations;
using DAL.Migrations;
using Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class Context : DbContext
    {
        // Your context has been configured to use a 'Model' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DAL.Model' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model' 
        // connection string in the application configuration file.
        public Context()
            : base("name=Context")
            //: base(@"data source=(LocalDb)\MSSQLLocalDB;initial catalog=DAL.CSharp;integrated security=True")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>(true));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<Person> People { get; }
    }
}