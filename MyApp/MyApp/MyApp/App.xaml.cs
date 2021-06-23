using MyApp.SQLite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp
{
    public partial class App : Application
    {
        private static SQLiteHelper _context;
        public static SQLiteHelper context
        {
            get
            {
                if (_context == null)
                    _context = new SQLiteHelper();

                return _context;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["bar-bg"],
                BarTextColor = (Color)Application.Current.Resources["bar-text"]
            };

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
