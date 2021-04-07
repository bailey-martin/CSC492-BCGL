using BCGL.ViewModels;
using BCGL.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BCGL
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(addItemsToDBPage), typeof(addItemsToDBPage));
            Routing.RegisterRoute(nameof(BarcodeScannerPage), typeof(BarcodeScannerPage));
            Routing.RegisterRoute(nameof(ScannerPage), typeof(ScannerPage));
        }

    }
}
