using BCGL.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BCGL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {
        public ScannerPage()
        {
            InitializeComponent();
            this.BindingContext = new ScannerPageViewModel();
        }

        protected override void OnDisappearing()
        {
            App.Database.selectedItem = null;
            base.OnDisappearing();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (App.Database.selectedItem != null)
            {
                collectionView.ItemsSource = await App.Database.GetBarcodesAsync(App.Database.selectedItem.listContent);
            }
            else
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}