using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BCGL
{
    [Table("UserList")]
    public class UserList
    {
        [PrimaryKey]
        public string listID { get; set; }
        [ForeignKey(typeof(UserData))]
        public string username { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public UserListDetailed[] UserListDetailed { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public UserData UserData { get; set; }
    }
}