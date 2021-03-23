using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BCGL
{
    [Table("UserListDetailed")]
    public class UserListDetailed
    {
        [ForeignKey(typeof(UserList))]
        public string listID { get; set; }
        public string listContent { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public UserList UserList { get; set; }
    }
}