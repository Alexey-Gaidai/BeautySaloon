using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BeautySaloon
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SplashScreen splashScreen = new SplashScreen("\\Resources\\img\\8568608.jpg");
            splashScreen.Show(true);
            // Закройте заставку
            splashScreen.Close(TimeSpan.FromMilliseconds(1000)); // Здесь можно задат
        }
    }
    
}
