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
            _database.CreateTableAsync<Barcode>().Wait();
            _database.CreateTableAsync<UserList>().Wait();
            _database.CreateTableAsync<UserListDetailed>().Wait();
        }

        public Task<List<UserList>> GetPeopleAsync()
        {
            return _database.Table<UserList>().ToListAsync();
        }

        public Task<int> SavePersonAsync(UserData userData)
        {
            return _database.InsertAsync(userData);
        }

        public Task<int> SaveInfoAsync(Barcode barcode)
        {
            return _database.InsertAsync(barcode);
        }

        public Task<int> DeletePersonAsync(UserData userData)
        {
            return _database.DeleteAsync(userData);
        }
    }
}