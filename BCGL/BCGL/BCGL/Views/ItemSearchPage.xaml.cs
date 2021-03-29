using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

using BCGL.ViewModels;
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
            if (string.IsNullOrWhiteSpace(searchTarget)) { collectionView.ItemsSource = await App.Database.GetBarcodesAllAsync(); }
            else { collectionView.ItemsSource = await App.Database.GetBarcodesAsync(searchTarget); }
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            searchFunction(searchBar.Text);
        }
        /**
         * Todo:
         * 1. Search bar functionality 
         *      - Add to list
         *      - - Be able to select item
         */
    }
}