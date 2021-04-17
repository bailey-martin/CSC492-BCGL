/*AppDelegate.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: This class was generated automatically by Visual Studio 2019 to support the application.
*/

using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace BCGL.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();

            //Barcode Scan NuGet Addition (allows for the camera barcode scanning functionality to function properly)
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            //End of Barcode Scan NuGet Addition
            LoadApplication(new App());

            //Firebase Addition (allows for the Firebase login/authentication mechanism to function properly)
            Firebase.Core.App.Configure();
            //End of Firebase Addition

            return base.FinishedLaunching(app, options);
        }
    }
}
