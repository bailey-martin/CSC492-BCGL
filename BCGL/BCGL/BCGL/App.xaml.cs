/*App.xaml.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: This class is designed to support the App.xaml class. It provides the global foundation for the entire application.
*/

using BCGL.Services;
using System;
using System.IO;
using Xamarin.Forms;

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
            // Debug Purposes
            // * Get DB Path
            //Console.WriteLine("################");
            //Console.WriteLine("################");
            //Console.WriteLine("################");
            //Console.WriteLine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)));
            //*/
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            await Shell.Current.GoToAsync("//LoginPage"); //upon app launch, take the user to the app's login page
        }

        protected override void OnSleep() {}

        protected override void OnResume() {}
    }
}
