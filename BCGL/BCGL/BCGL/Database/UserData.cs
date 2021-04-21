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
    [Table("UserData")]  // Create idenitfier for table for when used in code
    public class UserData
    {
        [PrimaryKey, Unique, MaxLength(20)] // Make varible directly below have a max length of 20 chars, unique, and a primary key
        public string username { get; set; }
        [MaxLength(12)] // Make varible directly below have a max length of 12 chars
        public string password { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)] // create foreign key realtions - when a record is removed, remove all other records using this refernece
        public UserList[] UserLists { get; set; } // create connection to UserList table
    }
}