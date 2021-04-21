using System;
using BCGL.ViewModels;
using Xamarin.Forms;

namespace BCGL.Views
{

    public partial class ItemSearchPage : ContentPage
    {

        ItemSearchViewModel _viewModel;

        public ItemSearchPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemSearchViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetBarcodesAllAsync(); // add items retrieved from DB to collectionView element
        }

        async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var selectedItemLocal = (Barcode)((StackLayout)sender).BindingContext;
            App.Database.selectedBarcodeItem = selectedItemLocal; // Update the global varible
            App.Database.fromPage = "ItemSearchPage";
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}");
        }

        public async void searchFunction(string searchTarget)
        {
            if (string.IsNullOrWhiteSpace(searchTarget))
            {
                collectionView.ItemsSource = await App.Database.GetBarcodesAllAsync(); // update items retrieved from DB to collectionView element
            }
            else
            {
                collectionView.ItemsSource = await App.Database.GetBarcodesAsync(searchTarget); // update items retrieved from DB to collectionView element
            }
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            searchFunction(searchBar.Text);
        }
    }
}