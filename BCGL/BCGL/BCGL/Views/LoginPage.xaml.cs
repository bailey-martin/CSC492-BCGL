using BCGL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;

namespace BCGL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class LoginPage : ContentPage
    {

        IAuth auth;

        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            auth = DependencyService.Get<IAuth>();
        }

        

        async void LoginClicked(object sender, EventArgs e)
        {
            String Token = await auth.LoginWithEmailPassword(UsernameInput.Text, PasswordInput.Text);
            if (Token != "")
            {
                //await Navigation.PushAsync(new AboutPage());    //testing purposes (proof of concept of successful login)
                //await Shell.Current.GoToAsync($"{nameof(ItemsPage)}");
                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
                //await Shell.Current.GoToAsync($"{nameof(AboutPage)}");
                //await DisplayAlert("auth success", "--", "OK");
            }
            else
            {
                ShowError();
            }
        }

        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
            //Console.WriteLine("LLLL");
        }
    }
}