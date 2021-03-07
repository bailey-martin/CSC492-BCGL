using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCGL.Services.Account;
using Firebase.Auth;
using BCGL.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(BCGL.Services.Account.AccountService))]
namespace BCGL.Droid.Services
{
    public class AccountService : IAccountService
    {
        public AccountService(){

        }

        public Task<double> GetCurrentPayRateAsync()
        {
            return Task.FromResult(10d);
        }

        public Task<bool> LoginAsync(string username, string password)
        {
            var tcs = new TaskCompletionSource<bool>();
            FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(username, password)
                .ContinueWith((task) => OnAuthCompleted(task, tcs));
            return tcs.Task;
        }

        private void OnAuthCompleted(Task task, TaskCompletionSource<bool> tcs)
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                //something went wrong
                tcs.SetResult(false);
                return;
            }
            tcs.SetResult(true);
        }
    }
}