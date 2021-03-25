using BCGL.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace BCGL.ViewModels
{
    public class CreateAccountModel : BaseViewModel
    {
        public Command CreateAccountCommands { get; }

        public CreateAccountModel()
        {
            CreateAccountCommands = new Command(OnCreateClicked);
        }

        private async void OnCreateClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(CreateAccountPage)}");
        }
    }
}

