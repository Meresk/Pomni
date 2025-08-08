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
            string tableNotesExistQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Notes'";

            var res = Database.SqlQuery<string>(tableNotesExistQuery).FirstOrDefault();
            if (res == null)
            {
                CreateNotesTable();
            }
        }

        public DbSet<Note> Notes { get; set; }

        private void CreateNotesTable()
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
