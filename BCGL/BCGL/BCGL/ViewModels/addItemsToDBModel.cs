using System;
using BCGL.Models;
using Xamarin.Forms;

namespace BCGL.ViewModels
{
    public class addItemsToDBModel : BaseViewModel
    {
        private int sku;
        private string productName;
        private decimal price;

        public addItemsToDBModel()
        {
            AddCommand = new Command(OnAdd, ValidateSave);
            this.PropertyChanged +=
                (_, __) => AddCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(sku.ToString())
                && !String.IsNullOrWhiteSpace(productName)
                && !String.IsNullOrWhiteSpace(price.ToString());
        }

        public int SKU
        {
            get => sku;
            set => SetProperty(ref sku, value);
        }

        public string ProductName
        {
            get => productName;
            set => SetProperty(ref productName, value);
        }

        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public Command AddCommand { get; }

        private async void OnAdd()
        {
            await App.Database.SaveInfoAsync(new Barcode
            {
                SKU = SKU,
                ProductName = ProductName,
                Price = Price
            });
            
        }
    }
}

