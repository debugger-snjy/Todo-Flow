using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// For DbContext Class
using System.Data.Entity;

namespace TodoApplication.Models.DataContext
{
    public class ContextData : DbContext
    {
        // This will set all the columns in the Database from the Users Model Class, For that we need Connection String
        public DbSet<TodoItem> Todos { get; set; }
    }
}