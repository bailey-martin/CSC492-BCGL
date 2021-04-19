using BCGL.Models;
using BCGL.ViewModels;
using Xamarin.Forms;

namespace BCGL.Views
{
    public partial class AddNewListPage : ContentPage
    {
        public Item Item { get; set; }

        public AddNewListPage()
        {
            InitializeComponent();
            BindingContext = new AddNewListViewModel();
        }
    }
}