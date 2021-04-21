/*AuthiOS.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: The AuthiOS class extends the IAuth interface and is designed to assist in the authentication/login process to the app. This bridges the connection between the app and Firebase.
*/

using System;
using System.Threading.Tasks;
using BCGL.iOS;
using Xamarin.Forms;
using Firebase.Auth;

[assembly: Dependency(typeof(AuthiOS))]
namespace BCGL.iOS
{
    public class AuthiOS : IAuth
    {
        public async Task<string> LoginWithEmailPassword(string email, string password) //try to sign in to the app with the credentials entered by the user
        {
            try
            {
                //[FIRAuth.auth useUserAccessGroup:nil error:nil];
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password); //authentication mechanism
                return await user.User.GetIdTokenAsync();
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}