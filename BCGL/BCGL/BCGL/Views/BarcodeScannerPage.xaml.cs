using Xamarin.Forms;

namespace BCGL.Views
{
    public partial class BarcodeScannerPage : ContentPage
    {

        public BarcodeScannerPage()
        {
            InitializeComponent();
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                scanResultText.Text = result.Text + " (type: " + result.BarcodeFormat.ToString() + ")";
                App.Database.scannerResult = result.Text;
                scannerView.IsScanning = false;
                Shell.Current.GoToAsync(".."); // Navigate to previous page
            });
        }

        protected override void OnDisappearing()
        {
            scannerView.IsScanning = false;
            base.OnDisappearing();
        }
    }
}