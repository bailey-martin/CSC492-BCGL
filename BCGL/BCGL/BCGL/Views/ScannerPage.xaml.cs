using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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