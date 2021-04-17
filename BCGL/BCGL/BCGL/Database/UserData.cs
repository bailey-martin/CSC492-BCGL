/*UserData.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: The UserData class is a table in the SQLite database that creates a UserData object for every end user. This stores their username and password, which are used after
     logging in to the application. The username value allows the BCGL app to display the specific user's shopping lists.
*/

using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BCGL
{
    [Table("UserData")]  //UserData table stored in the SQLite database
    public class UserData
    {
        [PrimaryKey, Unique, MaxLength(20)]
        public string username { get; set; }
        [MaxLength(12)]
        public string password { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)] //WHAT DOES THIS DO????
        public UserList[] UserLists { get; set; } //WHAT DOES THIS DO????
    }
}