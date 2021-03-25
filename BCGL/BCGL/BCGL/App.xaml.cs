using BCGL.Services;
using BCGL.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BCGL
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyDB_Final.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            ///**
            // * Get DB Path
            Console.WriteLine("################");
            Console.WriteLine("################");
            Console.WriteLine("################");
            Console.WriteLine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)));
            //*/
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            //MainPage = new NavigationPage(new LoginPage());
        }

        /*
        Task InitNavigation()
        {
            var navService = PageModelLocator.Resolve<INavigationService>();
            return navService.NavigateToAsync<LoginPageModel>();
        }
        */

        protected override async void OnStart()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        /*
        protected override async void OnStart()
        {
            await InitNavigation();
        }
        */

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
