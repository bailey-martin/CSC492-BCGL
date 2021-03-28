using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

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

        void searchButtonClicked(System.Object sender, System.EventArgs e)
        {
            var searchText = searchTextField.Text;
            Console.WriteLine("The returned text is: " + searchText);

            //call search db method with the contents of the search text field
            Console.WriteLine(App.Database.GetBarcodesAsync(searchText).ToString());
        }

        /*protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection myConnection = new SQLiteConnection(App.FilePath))
            {
                myConnection.CreateTable<Barcode>();
                var barcodes = myConnection.Table<Barcode>().ToList();
            }
        } */
    }
}