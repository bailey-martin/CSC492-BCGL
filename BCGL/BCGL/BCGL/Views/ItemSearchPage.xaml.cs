using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BCGL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemSearchPage : ContentPage
    {
        public ItemSearchPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            collectionView.ItemsSource = await App.Database.GetBarcodesAllAsync(); // add items retrieved from DB to collectionView element
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