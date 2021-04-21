using System;
using BCGL.ViewModels;
using Xamarin.Forms;

namespace BCGL.Views
{
    public partial class AddItemsToDBPage : ContentPage
    {

        AddItemsToDBViewModel _viewModel;
        public AddItemsToDBPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AddItemsToDBViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetInfoAsync(); // add items retrieved from DB to collectionView element
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            // Check if inputs are filled
            if (!string.IsNullOrWhiteSpace(productNameEntry.Text) && !string.IsNullOrWhiteSpace(skuEntry.Text) && !string.IsNullOrEmpty(priceEntry.Text))
            {
                productNameEntry.Text = string.Empty;
                skuEntry.Text = string.Empty;
                priceEntry.Text = string.Empty;
                collectionView.ItemsSource = await App.Database.GetInfoAsync();
            }
        }
    }
}
