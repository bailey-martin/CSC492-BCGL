using System;
using System.Collections.Generic;
using BCGL.Models;
using BCGL.ViewModels;
using Xamarin.Forms;

namespace BCGL.Views
{
    public partial class addItemsToDBPage : ContentPage
    {

        addItemsToDBModel _viewModel;
        public addItemsToDBPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new addItemsToDBModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetInfoAsync();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(productNameEntry.Text) && !string.IsNullOrWhiteSpace(skuEntry.Text))
            {
                productNameEntry.Text = string.Empty;
                skuEntry.Text = string.Empty;
                collectionView.ItemsSource = await App.Database.GetInfoAsync();
            }
        }
    }
}
