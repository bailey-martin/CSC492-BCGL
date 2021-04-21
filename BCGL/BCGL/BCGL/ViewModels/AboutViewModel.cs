/*AboutViewModel.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: The AboutViewModel provides the in-app web browser functionality for the 2 buttons on the "About" page of the BCGL app.
*/

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
            OpenWebContactCommand = new Command(async () => await Browser.OpenAsync("https://bailey-martin.github.io/CSC492-BCGL/about/#contact-us"));
        }

        public ICommand OpenWebCommand { get; } //in-app web browser functionality
        public ICommand OpenWebContactCommand { get; } //in-app web browser functionality
    }
}