using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BCGL.ViewModels;

namespace BCGL.Views
{
    public partial class CreateAccountPage : ContentPage
    {
        public CreateAccountPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(usernameEntry.Text) && !string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                await App.Database.SavePersonAsync(new UserData
                {
                    username = usernameEntry.Text,
                    password = passwordEntry.Text
                });

                usernameEntry.Text = passwordEntry.Text = string.Empty;
            }
        }
    }
}
