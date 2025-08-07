using Pomni.Models;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace Pomni.Data
{
    public class PomniDbContext : DbContext
    {
        public PomniDbContext() : base("name=PomniDb")
        {
            string ihateEF6 = "SELECT name FROM sqlite_master WHERE type='table' AND name='Notes'";

            var res = Database.SqlQuery<string>(ihateEF6).FirstOrDefault();
            if (res == null)
            {
                CreateNotesTables();
            }
        }

        public DbSet<Note> Notes { get; set; }

        private void CreateNotesTables()
        {
            string createTableSql = @"
            CREATE TABLE Notes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title NVARCHAR(255) NOT NULL,
                Content TEXT,
                ReminderDateTime DATETIME
            )";

            Database.ExecuteSqlCommand(createTableSql);
        }
    }
}
