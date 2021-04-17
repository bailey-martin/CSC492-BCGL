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
    [Table("UserListDetailed")] //UserListDetailed table located in the SQLite database
    public class UserListDetailed
    {
        [ForeignKey(typeof(UserList))]
        public string listID { get; set; }  //unique identifier for each shopping list
        public string listContent { get; set; } //WHAT IS THIS??
        [PrimaryKey]
        public string record { get; set; } //WHAT IS THIS??

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)] //WHAT IS THIS???
        public UserList UserList { get; set; } //WHAT IS THIS???
    }
}