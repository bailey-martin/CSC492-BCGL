using BCGL.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BCGL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage()
        {
            InitializeComponent();
            this.BindingContext = new ProductDetailViewModel();
        }

        protected override void OnDisappearing()
        {
            App.Database.selectedItem = null; // clear the global variable
            App.Database.selectedBarcodeItem = null; // clear the global variable
            App.Database.fromPage = null; // clear the global variable
            base.OnDisappearing();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (App.Database.fromPage == "ListDetailedPage" && App.Database.selectedItem != null)
            {
                collectionView.ItemsSource = await App.Database.GetBarcodesAsync(App.Database.selectedItem.listContent); // add items retrieved from DB to collectionView element
            }
            else if (App.Database.fromPage == "ItemSearchPage" && App.Database.selectedBarcodeItem != null)
            {
                collectionView.ItemsSource = await App.Database.GetBarcodesAsync(App.Database.selectedBarcodeItem.SKU); // add items retrieved from DB to collectionView element
            }
            else
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}