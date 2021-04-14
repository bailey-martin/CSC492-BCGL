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
            LoginCommand = new Command(OnLoginClicked);
        }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"{nameof(ItemsPage)}");
        }
    }
}
