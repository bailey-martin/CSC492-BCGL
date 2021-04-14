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

            collectionView.ItemsSource = await App.Database.GetBarcodesAllAsync();
        }

        public async void searchFunction(string searchTarget)
        {
            if (string.IsNullOrWhiteSpace(searchTarget))
            {
                collectionView.ItemsSource = await App.Database.GetBarcodesAllAsync();
            }
            else
            {
                collectionView.ItemsSource = await App.Database.GetBarcodesAsync(searchTarget);
            }
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            searchFunction(searchBar.Text);
        }
    }
}