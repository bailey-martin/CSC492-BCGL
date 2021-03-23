using System;
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

        public Task<List<UserListDetailed>> GetListDetailedAsync(string listID)
        {
            return _database.Table<UserListDetailed>().Where(items => items.listID == listID).ToListAsync();
        }

        public Task<List<UserList>> GetListAsync(string username)
        {
            return _database.Table<UserList>().Where(items => items.username == username).ToListAsync();
        }

        public Task<int> SavePersonAsync(UserData userData)
        {
            return _database.InsertAsync(userData);
        }

        public Task<int> SaveListAsync(UserList userList)
        {
            return _database.InsertAsync(userList);
        }

        public Task<int> SaveListDetailedAsync(UserListDetailed userListDetailed)
        {
            return _database.InsertAsync(userListDetailed);
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