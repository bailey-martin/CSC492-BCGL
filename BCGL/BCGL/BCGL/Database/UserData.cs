using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BCGL
{
    [Table("UserData")]
    public class UserData
    {
        [PrimaryKey, Unique, MaxLength(20)]
        public string username { get; set; }
        [MaxLength(12)]
        public string password { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public UserList[] UserLists { get; set; }
    }
}