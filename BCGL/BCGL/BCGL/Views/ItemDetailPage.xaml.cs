using BCGL.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace BCGL.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel _viewModel;
        UserListDetailed listItem;
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemDetailViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetListDetailedAsync(_viewModel.ItemId);
            if (App.Database.scannerResult != null)
            {
                string barcode = App.Database.scannerResult;
                Barcode barcodeItem = await App.Database.GetBarcodeAsync(barcode);
                if (barcodeItem == null)
                {
                    searchTextField.Text = "No Item Found";
                }
                else
                {
                    searchTextField.Text = barcodeItem.ProductName;
                }
                App.Database.scannerResult = null;
            }
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (listItem != null)
            {
                await App.Database.SaveListDetailedAsync(listItem);
                collectionView.ItemsSource = await App.Database.GetListDetailedAsync(_viewModel.ItemId);
                listItem = null;
            }
        }

        async void OnButtonTwoClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(BarcodeScannerPage)}");
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

        public async void searchFunction(string searchTarget)
        {
            if (!string.IsNullOrWhiteSpace(searchTarget))
            { searchResults.ItemsSource = await App.Database.GetBarcodesNameAsync(searchTarget); }
            else { searchResults.ItemsSource = ""; }
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            searchFunction(searchBar.Text);
        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Barcode item = (Barcode)((StackLayout)sender).BindingContext;
            listItem = new UserListDetailed
            {
                listID = _viewModel.ItemId,
                listContent = item.ProductName,
                record = Guid.NewGuid().ToString()
            };
        }
        async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var selectedItemLocal = (UserListDetailed)((StackLayout)sender).BindingContext;
            App.Database.selectedItem = selectedItemLocal;
            await Shell.Current.GoToAsync($"{nameof(ScannerPage)}");
            Console.WriteLine(selectedItemLocal.listContent);
        }
    }
}

