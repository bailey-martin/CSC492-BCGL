using BCGL.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace BCGL.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel _viewModel;
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemDetailViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetListDetailedAsync();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(productEntry.Text))
            {
                await App.Database.SaveListDetailedAsync(new UserListDetailed
                {
                    listID = _viewModel.ItemId,
                    listContent = productEntry.Text
                });

                productEntry.Text = string.Empty;
                collectionView.ItemsSource = await App.Database.GetListDetailedAsync();
            }
        }
    }
}

