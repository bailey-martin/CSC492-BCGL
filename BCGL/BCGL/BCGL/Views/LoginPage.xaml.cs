using BCGL.ViewModels;
using System;
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
            // catch general errors so app doesnt crash
            try
            {
                Token = await auth.LoginWithEmailPassword(UsernameInput.Text, PasswordInput.Text);
                if (Token != "")
                {
                    string username = UsernameInput.Text.ToLower();
                    await App.Database.SavePersonAsync(new UserData
                    {
                        username = username,
                        password = PasswordInput.Text
                    });
                    _viewModel.Username = username;
                    App.Database.username = username; // update global variable
                    await Shell.Current.GoToAsync($"{nameof(ListPage)}");
                }
                else 
                {
                    ShowError();
                }
                
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                ShowError();
            }
        }

        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
        }
    }
}