/*UserListDetailed.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: The UserListDetailed class represents a table of UserListDetailed objects in the SQLite database. The listID is a key set on each shopping list and allows for the system to
      display the user's desired shopping list. The listContent, record, and UserList??????????????????????????????????]    <<<<<<<<COME BACK>>>>>>>>>>
*/

using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BCGL
{
    [Table("UserListDetailed")] // Create idenitfier for table for when used in code
    public class UserListDetailed
    {
        [ForeignKey(typeof(UserList))] // set foreign key relation to variable below
        public string listID { get; set; }  // store the list id to know what list this item belongs to
        public string listContent { get; set; } // store the list item found from the database table
        [PrimaryKey] // set variable below a primary key
        public string record { get; set; } // store a unique identifer

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)] // create foreign key realtions - when a record is removed, remove all other records using this refernece
                                                                      // Set how many records belong to each other
        public UserList UserList { get; set; } // finish connection to UserList table
    }
}