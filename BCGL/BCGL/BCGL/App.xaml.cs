using BCGL.PageModels;
using BCGL.PageModels.Base;
using BCGL.Services;
using BCGL.Services.Navigation;
using BCGL.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BCGL
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
