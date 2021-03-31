using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace BCGL
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;
        private string username1;
        private string result;
        public string username { get => username1; set => username1 = value; }
        public string scannerResult { get => result; set => result = value; }

        public Database(string dbPath)
        {
            /**
             * userData - password/username, username Unique
             * userList - username/listId
             * userListDetailed - listId/listContent
             */
            _database = new SQLiteAsyncConnection(dbPath);

            //_database.DeleteAllAsync<UserListDetailed>().Wait();
            //_database.DropTableAsync<UserListDetailed>().Wait();

            _database.CreateTableAsync<Barcode>().Wait();
            _database.CreateTableAsync<UserData>().Wait();
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

        //

        public Task<List<Barcode>> GetBarcodesAllAsync()
        {
            return _database.Table<Barcode>().ToListAsync();
        }

        public Task<List<Barcode>> GetBarcodesAsync(string barcodeNumber)
        {
            Console.WriteLine("Received barcode: " + barcodeNumber);
            int barcodeNumberConverted = 0;
            try
            {
                barcodeNumberConverted = Int32.Parse(barcodeNumber);
                Console.WriteLine("Post conversion barcode: " + barcodeNumber);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            return _database.Table<Barcode>().Where(items => items.SKU == barcodeNumberConverted).ToListAsync();
            //return _database.Table<Barcode>().Where(i => i.SKU == barcodeNumberConverted).FirstOrDefaultAsync();
        }

        public Task<List<Barcode>> GetBarcodesNameAsync(string productName)
        {
            return _database.Table<Barcode>().Where(items => items.ProductName.ToLower() == productName.ToLower()).ToListAsync();
        }

        //

        public Task<int> SavePersonAsync(UserData userData)
        {
            return _database.InsertOrReplaceAsync(userData);

            //return _database.InsertAsync(userData);
        }

        public Task<int> SaveListAsync(UserList userList)
        {
            return _database.InsertAsync(userList);
        }

        public Task<int> UpdateListAsync(UserList userList)
        {
            return _database.UpdateAsync(userList);
        }

        public Task<int> SaveListDetailedAsync(UserListDetailed userListDetailed)
        {
            return _database.InsertAsync(userListDetailed);
        }

        public Task<int> SaveInfoAsync(Barcode barcode)
        {
            return _database.InsertAsync(barcode);
        }

        public Task<List<Barcode>> GetInfoAsync()
        {
            return _database.Table<Barcode>().ToListAsync();
        }

        public Task<int> DeletePersonAsync(UserData userData)
        {
            return _database.DeleteAsync(userData);
        }


        public Task<int> DeleteListAsync(UserList userList)
        {
            return _database.DeleteAsync(userList);
        }

        public Task<int> DeleteListDetailedAsync(UserListDetailed userListDetailed)
        {
            return _database.DeleteAsync(userListDetailed);
        }
    }
}