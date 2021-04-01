using System;
using BCGL.Models;
using Xamarin.Forms;

namespace BCGL.ViewModels
{
    public class addItemsToDBModel : BaseViewModel
    {
        private string sku;
        private string productName;
        private decimal price;

        public addItemsToDBModel()
        {
            sku = "";
            productName = "";
            AddCommand = new Command(OnAdd, ValidateSave);
            this.PropertyChanged +=
                (_, __) => AddCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(sku)
                && !String.IsNullOrWhiteSpace(productName)
                && !String.IsNullOrWhiteSpace(price.ToString())
                && !String.IsNullOrEmpty(price.ToString())
                && decimal.Parse(price.ToString()) > 0
                && sku.Length == 12;
        }

        public string SKU
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

