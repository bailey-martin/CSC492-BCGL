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
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                //var token = await user.User.GetIdTokenAsync(false);
                var token = await (FirebaseAuth.Instance.CurrentUser.GetIdToken(true).AsAsync<GetTokenResult>());
                return token.Token;
            }
            catch(FirebaseAuthInvalidUserException e){
                e.PrintStackTrace();
                return "";
            }
        }
    }
}