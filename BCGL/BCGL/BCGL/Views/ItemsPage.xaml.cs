using BCGL.ViewModels;
using Xamarin.Forms;

namespace BCGL.Views
{
    public partial class ItemsPage : ContentPage
    {
        ListViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}