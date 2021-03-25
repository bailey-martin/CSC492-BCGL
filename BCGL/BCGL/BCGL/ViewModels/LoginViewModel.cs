using BCGL.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BCGL.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public Command CreateAccountCommand { get; }

        private string username;

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            CreateAccountCommand = new Command(OnCreateClicked);
        }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }

        private async void OnCreateClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"{nameof(CreateAccountPage)}");
        }
    }
}
