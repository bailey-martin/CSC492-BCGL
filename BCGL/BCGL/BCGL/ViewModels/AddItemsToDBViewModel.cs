/*addItemsToDBModel.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: This class is designed to assist in the process of adding product items to the app database. Every item contains a SKU, product name, and price.
*/

using System;
using Xamarin.Forms;

namespace BCGL.ViewModels
{
    public class AddItemsToDBViewModel : BaseViewModel
    {
        private string sku;
        private string productName;  //product information
        private double price;

        public AddItemsToDBViewModel()
        {
            sku = "";
            productName = "";
            AddCommand = new Command(OnAdd, ValidateSave);
            this.PropertyChanged +=
                (_, __) => AddCommand.ChangeCanExecute();
            Title = "Add Products to System"; //GUI element
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(sku) //checks the following values to ensure that they are not null and that each entity consists of more than a single space character
                && !String.IsNullOrWhiteSpace(productName)
                && !String.IsNullOrWhiteSpace(price.ToString())
                && !String.IsNullOrEmpty(price.ToString())
                && double.Parse(price.ToString()) > 0 //ensures price is valid
                && sku.Length == 12; //checks to ensure that the SKU number is 12 characters long (this is a universal rule/format)
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

        public double Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public Command AddCommand { get; } //addition of products/items

        private async void OnAdd()
        {
            await App.Database.SaveInfoAsync(new Barcode //saves the item into the app's database when the user presses the Add button
            {
                SKU = SKU,
                ProductName = ProductName,
                Price = Price
            });
            
        }

    }
}