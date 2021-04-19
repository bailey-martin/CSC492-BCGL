/*ItemsViewModel.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: The ItemViewModel class is the engine used for products within the BCGL app. This class provides functionality for swipe gestures and programs the app's response to user
      input.
*/

using BCGL.Models;
using BCGL.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BCGL.ViewModels
{
    public class ListViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; } //references XAML view and is the link between a view and viewWModel
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command<Item> ItemSwipedLeft { get; }
        public Command<Item> ItemSwipedRight { get; }


        public ListViewModel()
        {
            Title = "My Lists";  //UI element
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);  //gestures
            ItemSwipedLeft = new Command<Item>(OnSwipedLeft);
            ItemSwipedRight = new Command<Item>(OnSwipedRight);

            AddItemCommand = new Command(OnAddItem);

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true; //indicates the operation is in progress

            try
            {
                Items.Clear();
                var username = App.Database.username; //user who is requesting a shopping list to be displayed

                var itemIDs = await App.Database.GetListAsync(username);
                foreach (var id in itemIDs)
                {
                    
                    Item newItem = new Item() //creates a new Item object for every item on the shopping list
                    {
                        Id = id.listID,
                        Text = id.text,
                        Description = id.description
                    };

                    Items.Add(newItem); //ass a new Item to the Items arrayList
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false; //indicates that the operation has completed
            }
        }

        public void OnAppearing()
        {
            IsBusy = true; //begins the operation when page is loaded/requested by user
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
                OnSwipedLeft(value);
                OnSwipedRight(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage)); //move UI to New Item Page
        }


        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ListDetailedViewModel.ItemId)}={item.Id}"); //display the Detailed View page for an item
        }

        async void OnSwipedLeft(Item item)
        {
            if (item == null)
                return;

            bool changeTitle = await App.Current.MainPage.DisplayAlert("Confirm:", "Do you want to edit the item title: " + item.Text + "?", "Yes", "No"); //edit title of item
            if (changeTitle)
            {
                string titleChange = await App.Current.MainPage.DisplayPromptAsync("Confirm:", "Change list title", "OK", "Cancel", item.Text); //edit list title
                if (string.IsNullOrEmpty(titleChange) || string.IsNullOrWhiteSpace(titleChange))
                {
                    titleChange = item.Text;
                }
                item.Text = titleChange;
            }
            bool changedescription = await App.Current.MainPage.DisplayAlert("Confirm:", "Do you want to edit the item description: " + item.Description + "?", "Yes", "No"); //edit item description
            if (changedescription)
            {
                string descriptionChange = await App.Current.MainPage.DisplayPromptAsync("Confirm:", "Change list description", "OK", "Cancel", item.Description);  //edit list description
                if (string.IsNullOrEmpty(descriptionChange) || string.IsNullOrWhiteSpace(descriptionChange))
                {
                    descriptionChange = item.Description;
                }
                item.Description = descriptionChange;
            }
            DataStore.UpdateItemAsync(item).Wait();
            await App.Database.UpdateListAsync(new UserList //sync the app database
            {
                listID = item.Id,
                username = App.Database.username,
                text = item.Text,
                description = item.Description
            });
            Items.Remove(item);
            Items.Add(item); //reload of the list items
        }

        async void OnSwipedRight(Item item)
        {
            if (item == null)
                return;

            bool answer = await App.Current.MainPage.DisplayAlert("Confirm:", "Are You sure you want to Delete the item: "+item.Text+"?", "Yes", "No"); //allows you to swipe right to delete an item from your list
            if (answer)
            {
                await DataStore.DeleteItemAsync(item.Id);
                await App.Database.DeleteListAsync(new UserList //sync the app database
                {
                    listID = item.Id,
                    username = App.Database.username,
                    text = item.Text,
                    description = item.Description
                });
                await ExecuteLoadItemsCommand();
            }
        }

    }
}