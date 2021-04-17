/*NewItemViewModel.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: The NewItemViewModel class provides the functionality for UI elements on the app's "New Item" page.
*/

using BCGL.Models;
using System;
using Xamarin.Forms;

namespace BCGL.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string text;
        private string description;
        private string id;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            Title = "Add New Shopping List"; //displayed on the UI
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description); //checks to make sure the new item is valid
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync(".."); //return to the previous page
        }

        private async void OnSave()
        {
            Item newItem = new Item() //creation of a new Item object
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Description = Description
            };

            await DataStore.AddItemAsync(newItem); //sync the database
            await App.Database.SaveListAsync(new UserList //save to the database
            {
                listID = newItem.Id,
                username = App.Database.username,
                text = Text,
                description = description
            });

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync(".."); //after save, you will return to the previous page in the app
        }
    }
}
