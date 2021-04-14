using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BCGL.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://bailey-martin.github.io/CSC492-BCGL/"));
            OpenWebCommand = new Command(async () => await Launcher.OpenAsync("https://bailey-martin.github.io/CSC492-BCGL/about/#contact-us"));
        }

        public ICommand OpenWebCommand { get; }
    }
}