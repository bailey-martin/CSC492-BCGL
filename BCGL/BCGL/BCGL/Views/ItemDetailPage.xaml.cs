using BCGL.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace BCGL.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
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
                    listID = int.Parse(nameof(ItemDetailViewModel.ItemId)),
                    listContent = productEntry.Text
                });

                productEntry.Text = string.Empty;
                collectionView.ItemsSource = await App.Database.GetListDetailedAsync();
            }
        }
    }
}

