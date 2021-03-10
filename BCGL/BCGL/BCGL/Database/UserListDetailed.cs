using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BCGL
{
    [Table("UserListDetailed")]
    public class UserListDetailed
    {
        [PrimaryKey, ForeignKey(typeof(UserList))]
        public int listID { get; set; }
        public int listContent { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public UserList UserList { get; set; }
    }
}