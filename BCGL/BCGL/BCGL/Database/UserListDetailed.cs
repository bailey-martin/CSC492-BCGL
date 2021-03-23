using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BCGL
{
    [Table("UserListDetailed")]
    public class UserListDetailed
    {
        [PrimaryKey, ForeignKey(typeof(UserList))]
        public int listID { get; set; }
        public string listContent { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public UserList UserList { get; set; }
    }
}