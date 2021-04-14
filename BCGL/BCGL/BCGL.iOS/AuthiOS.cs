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
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                //[FIRAuth.auth useUserAccessGroup:nil error:nil];
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return await user.User.GetIdTokenAsync();
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}