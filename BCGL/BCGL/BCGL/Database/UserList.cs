/*UserList.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: The UserList class represents the UserList table in the SQLite database. This table contains all of the information about a user and allows for their UserData to be set or retrieved.
      The following values are stored: listID, username, text, description, and their UserData.
*/

using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BCGL
{
    [Table("UserList")] //UserList table in the SQLite database
    public class UserList
    {
        [PrimaryKey]
        public string listID { get; set; }
        [ForeignKey(typeof(UserData))]
        public string username { get; set; }
        public string text { get; set; }
        public string description { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)] //WHAT DOES THIS DO?????
        public UserListDetailed[] UserListDetailed { get; set; } //WHAT DOES THIS DO???

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)] //WHAT DOES THIS DO????
        public UserData UserData { get; set; } //WHAT DOES THIS DO????
    }
}