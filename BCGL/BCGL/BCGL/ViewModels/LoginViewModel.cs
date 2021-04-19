/*LoginViewModel.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: This class provides functionality for the Login page of the BCGL app.
*/

using BCGL.Views;
using Xamarin.Forms;

namespace BCGL.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        private string username;

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked); //executing the login process
        }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"{nameof(ListPage)}"); //move to the Items page (the shopping list page) after signing in
        }
    }
}
