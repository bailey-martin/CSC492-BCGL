using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace BCGL
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            /**
             * userData - password/username, username Unique
             * userList - username/listId
             * userListDetailed - listId/listContent
             */
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserData>().Wait();
            _database.CreateTableAsync<UserList>().Wait();
            _database.CreateTableAsync<UserListDetailed>().Wait();
        }

        public Task<List<UserList>> GetPeopleAsync()
        {
            return _database.Table<UserList>().ToListAsync();
        }

        public Task<int> SavePersonAsync(UserList userList)
        {
            return _database.InsertAsync(userList);
        }

        public Task<int> DeletePersonAsync(UserList userList)
        {
            return _database.DeleteAsync(userList);
        }
    }
}