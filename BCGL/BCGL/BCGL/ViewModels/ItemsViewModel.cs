﻿using BCGL.Models;
using BCGL.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BCGL.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command<Item> ItemSwipedLeft { get; }
        public Command<Item> ItemSwipedRight { get; }


        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);
            ItemSwipedLeft = new Command<Item>(OnSwipedLeft);
            ItemSwipedRight = new Command<Item>(OnSwipedRight);

            AddItemCommand = new Command(OnAddItem);

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var username = App.Database.username;

                var itemIDs = await App.Database.GetListAsync(username);
                foreach (var id in itemIDs)
                {
                    
                    Item newItem = new Item()
                    {
                        Id = id.listID,
                        Text = id.text,
                        Description = id.description
                    };

                    Items.Add(newItem);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
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
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }


        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        async void OnSwipedLeft(Item item)
        {
            if (item == null)
                return;

            Console.WriteLine("Left");
            //bool answer = await App.Current.MainPage.DisplayAlert("Question?", "Are You sure you want to Edit the item: " + item.Text + "?", "Yes", "No");
            //if (answer)
            //{
            //    // Go to new Screen
            //    string titleChange = await App.Current.MainPage.DisplayPromptAsync("Change list title:", "Change list title", "OK", "Cancel", item.Text);
            //    string descriptionChange = await App.Current.MainPage.DisplayPromptAsync("Change list description:", "Change list description", "OK", "Cancel", item.Description);
            //    if (string.IsNullOrEmpty(titleChange) || string.IsNullOrWhiteSpace(titleChange))
            //    {
            //        titleChange = item.Text;
            //    }
            //    if (string.IsNullOrEmpty(descriptionChange) || string.IsNullOrWhiteSpace(descriptionChange))
            //    {
            //        descriptionChange = item.Description;
            //    }
            //    item.Text = titleChange;
            //    item.Description = descriptionChange;

            //    await DataStore.UpdateItemAsync(item);
            //    await App.Database.UpdateListAsync(new UserList
            //    {
            //        listID = item.Id,
            //        username = App.Database.username,
            //        text = item.Text,
            //        description = item.Description
            //    });
            //    Items.Remove(item);
            //    Items.Add(item);
            //}

            bool changeTitle = await App.Current.MainPage.DisplayAlert("Question?", "Do you want to edit the item title: " + item.Text + "?", "Yes", "No");
            if (changeTitle)
            {
                string titleChange = await App.Current.MainPage.DisplayPromptAsync("Change list title:", "Change list title", "OK", "Cancel", item.Text);
                if (string.IsNullOrEmpty(titleChange) || string.IsNullOrWhiteSpace(titleChange))
                {
                    titleChange = item.Text;
                }
                item.Text = titleChange;
            }
            bool changedescription = await App.Current.MainPage.DisplayAlert("Question?", "Do you want to edit the item description: " + item.Description + "?", "Yes", "No");
            if (changedescription)
            {
                string descriptionChange = await App.Current.MainPage.DisplayPromptAsync("Change list description:", "Change list description", "OK", "Cancel", item.Description);
                if (string.IsNullOrEmpty(descriptionChange) || string.IsNullOrWhiteSpace(descriptionChange))
                {
                    descriptionChange = item.Description;
                }
                item.Description = descriptionChange;
            }
            DataStore.UpdateItemAsync(item).Wait();
            await App.Database.UpdateListAsync(new UserList
            {
                listID = item.Id,
                username = App.Database.username,
                text = item.Text,
                description = item.Description
            });
            Items.Remove(item);
            Items.Add(item);
        }

        async void OnSwipedRight(Item item)
        {
            if (item == null)
                return;

            bool answer = await App.Current.MainPage.DisplayAlert("Question?", "Are You sure you want to Delete the item: "+item.Text+"?", "Yes", "No");
            if (answer)
            {
                await DataStore.DeleteItemAsync(item.Id);
                await App.Database.DeleteListAsync(new UserList
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