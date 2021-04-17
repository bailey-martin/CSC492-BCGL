/*IAuth.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: This interface supports and implements the app's Firebase login/authentication mechanism.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BCGL
{
    public interface IAuth
    {
        Task<string> LoginWithEmailPassword(string email, string password);
    }
}