/*AuthDroid.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: The AuthDroid class extends the IAuth interface and is designed to assist in the authentication/login process to the app. This bridges the connection between the app and Firebase.
*/

using System.Threading.Tasks;
using Xamarin.Forms;
using BCGL.Droid;
using Firebase.Auth;
using Android.Gms.Extensions;

[assembly: Dependency(typeof(AuthDroid))]
namespace BCGL.Droid
{
    public class AuthDroid : IAuth
    {
        public async Task<string> LoginWithEmailPassword(string email, string password) //try to sign in to the app with the credentials entered by the user
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password); //authentication mechanism
                //var token = await user.User.GetIdTokenAsync(false);
                var token = await (FirebaseAuth.Instance.CurrentUser.GetIdToken(true).AsAsync<GetTokenResult>()); //authentication mechanism
                return token.Token;
            }
            catch(FirebaseAuthInvalidUserException e){
                e.PrintStackTrace(); //debugging purposes
                return "";
            }
        }
    }
}