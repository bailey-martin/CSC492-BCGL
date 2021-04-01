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
            _database = new SQLiteAsyncConnection(dbPath);

            //_database.DeleteAllAsync<UserListDetailed>().Wait();
            //_database.DropTableAsync<UserListDetailed>().Wait();
            //_database.DeleteAllAsync<Barcode>().Wait();
            //_database.DropTableAsync<Barcode>().Wait();

            _database.CreateTableAsync<Barcode>().Wait();
            _database.CreateTableAsync<UserData>().Wait();
            _database.CreateTableAsync<UserList>().Wait();
            _database.CreateTableAsync<UserListDetailed>().Wait();
        }

        // USED - set specific lists
        public Task<List<UserListDetailed>> GetListDetailedAsync(string listID)
        {
            return _database.Table<UserListDetailed>().Where(items => items.listID == listID).ToListAsync();
        }

        // USED - set intial items on homepage
        public Task<List<UserList>> GetListAsync(string username)
        {
            return _database.Table<UserList>().Where(items => items.username == username).ToListAsync();
        }

        // USED? - on search page initally display all products available
        public Task<List<Barcode>> GetBarcodesAllAsync()
        {
            return _database.Table<Barcode>().ToListAsync();
        }

        // Only used on itemSearchPage
        public Task<List<Barcode>> GetBarcodesAsync(string barcodeNumber)
        {

            return _database.QueryAsync<Barcode>("Select * FROM Barcode WHERE ProductName LIKE ? OR SKU LIKE ?", "%" + barcodeNumber + "%", "%" + barcodeNumber + "%");
            //return _database.Table<Barcode>().Where(items => items.SKU == barcodeNumber).ToListAsync();
        }

        // Used - retrive barcode object from sku
        public Task<Barcode> GetBarcodeAsync(string barcodeNumber)
        {
            return _database.Table<Barcode>().Where(items => items.SKU == barcodeNumber).FirstOrDefaultAsync();
        }

        // USED - display all record that match a product name
        public Task<List<Barcode>> GetBarcodesNameAsync(string productName)
        {
            return _database.QueryAsync<Barcode>("Select * FROM Barcode WHERE ProductName LIKE ?", "%" + productName + "%");
        }

        // USED - local copy of userdata to bind lists to
        public Task<int> SavePersonAsync(UserData userData)
        {
            return _database.InsertOrReplaceAsync(userData);
        }

        // USED - save user custom list
        public Task<int> SaveListAsync(UserList userList)
        {
            return _database.InsertAsync(userList);
        }

        // USED - update user custom list elements
        public Task<int> UpdateListAsync(UserList userList)
        {
            return _database.UpdateAsync(userList);
        }

        // USED - add item to detailed custom list
        public Task<int> SaveListDetailedAsync(UserListDetailed userListDetailed)
        {
            return _database.InsertAsync(userListDetailed);
        }

        // USED only to add barcode items
        public Task<int> SaveInfoAsync(Barcode barcode)
        {
            return _database.InsertAsync(barcode);
        }

        // USED only to display items in barcode db
        public Task<List<Barcode>> GetInfoAsync()
        {
            return _database.Table<Barcode>().ToListAsync();
        }

        // USED - delete a custom list
        public Task<int> DeleteListAsync(UserList userList)
        {
            _database.Table<UserListDetailed>().DeleteAsync(item => item.listID == userList.listID);
            return _database.DeleteAsync(userList);
        }

        // USED - delete item from user specific list
        public Task<int> DeleteListDetailedAsync(UserListDetailed userListDetailed)
        {
            return _database.DeleteAsync(userListDetailed);
        }
    }
}