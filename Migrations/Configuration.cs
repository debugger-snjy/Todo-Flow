namespace TodoApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TodoApplication.Models.DataContext.ContextData>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TodoApplication.Models.DataContext.ContextData";
        }

        protected override void Seed(TodoApplication.Models.DataContext.ContextData context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
