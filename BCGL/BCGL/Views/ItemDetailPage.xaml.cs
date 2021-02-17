using BCGL.ViewModels;
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
    }
}