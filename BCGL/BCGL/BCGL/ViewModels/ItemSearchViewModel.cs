/*ItemsViewModel.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: The ItemViewModel class is the engine used for products within the BCGL app. This class provides functionality for swipe gestures and programs the app's response to user
      input.
*/
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using BCGL.Models;
using BCGL.Views;
using Xamarin.Forms;

namespace BCGL.ViewModels
{
    public class ItemSearchViewModel : BaseViewModel
    {
        public ItemSearchViewModel()
        {
            Title = "Product Search";  //UI element
        }

    }
}
