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
        LoginViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new LoginViewModel();
            auth = DependencyService.Get<IAuth>();
            Shell.SetTabBarIsVisible(this, false);
        }

        

        async void LoginClicked(object sender, EventArgs e)
        {
            String Token = "";
            try
            {
                Token = await auth.LoginWithEmailPassword(UsernameInput.Text, PasswordInput.Text);
                if (Token != "")
                {
                    await App.Database.SavePersonAsync(new UserData
                    {
                        username = UsernameInput.Text,
                        password = PasswordInput.Text
                    });

                    Console.WriteLine(UsernameInput.Text);
                    Console.WriteLine("################");
                    Console.WriteLine("----------------");
                    Console.WriteLine("################");
                    Console.WriteLine("****************");
                    Console.WriteLine("################");
                    _viewModel.Username = UsernameInput.Text;
                    App.Database.username = UsernameInput.Text;
                    await Shell.Current.GoToAsync($"{nameof(ItemsPage)}");
                }
                else
                {
                    ShowError();
                }
                
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                Console.WriteLine("#############");
                Console.WriteLine("#############");
                Console.WriteLine("#############");
                Console.WriteLine("#############");
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