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
    [Table("UserList")] // Create idenitfier for table for when used in code
    public class UserList
    {
        [PrimaryKey] // set variable below a primary key
        public string listID { get; set; }
        [ForeignKey(typeof(UserData))] // set foreign key relation to variable below
        public string username { get; set; }
        public string text { get; set; }
        public string description { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)] // create foreign key realtions - when a record is removed, remove all other records using this refernece
                                                              // Set how many records belong to each other
        public UserListDetailed[] UserListDetailed { get; set; } // create connection to UserList table

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)] // create foreign key realtions - when a record is removed, remove all other records using this refernece
                                                                      // Set how many records belong to each other
        public UserData UserData { get; set; } // finish connection to UserData table
    }
}