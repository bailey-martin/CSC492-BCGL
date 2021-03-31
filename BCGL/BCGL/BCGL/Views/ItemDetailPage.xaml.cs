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
            collectionView.ItemsSource = await App.Database.GetListDetailedAsync(_viewModel.ItemId);
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(productEntry.Text))
            {
                await App.Database.SaveListDetailedAsync(new UserListDetailed
                {
                    listID = _viewModel.ItemId,
                    listContent = productEntry.Text,
                    record = Guid.NewGuid().ToString()
                });

                productEntry.Text = string.Empty;
                collectionView.ItemsSource = await App.Database.GetListDetailedAsync(_viewModel.ItemId);
            }
        }

        async void OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    // Handle the swipe
                    UserListDetailed item = (UserListDetailed)e.Parameter;
                    bool answer = await DisplayAlert("Question?","Are your sure you want to delete this item: "+ item.listContent, "Yes","No");
                    if (answer)
                    {
                        await App.Database.DeleteListDetailedAsync(new UserListDetailed
                        {
                            listID = _viewModel.ItemId,
                            listContent = item.listContent,
                            record = item.record
                        });
                        collectionView.ItemsSource = await App.Database.GetListDetailedAsync(_viewModel.ItemId);
                    }
                    break;
            }
        }
    }
}

