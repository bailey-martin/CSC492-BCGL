using BCGL.Models;
using BCGL.ViewModels;
using Xamarin.Forms;

namespace BCGL.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}