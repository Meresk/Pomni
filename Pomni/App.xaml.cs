using Pomni.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Pomni
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            using (var db = new PomniDbContext())
            {
                try
                {
                    _ = db.Notes.FirstOrDefault();
                }
                catch
                {
                    MessageBox.Show("Ошибка доступа к базе данных");
                    Shutdown();
                }
            }

            base.OnStartup(e);
        }
    }
}
