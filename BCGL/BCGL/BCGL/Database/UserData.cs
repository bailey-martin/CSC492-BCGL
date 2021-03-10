using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BCGL
{
    [Table("UserData")]
    public class UserData
    {
        [PrimaryKey, Unique, MaxLength(20)]
        public string userName { get; set; }
        [MaxLength(12)]
        public int password { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public UserList[] UserLists { get; set; }
    }
}