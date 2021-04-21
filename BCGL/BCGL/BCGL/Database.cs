/*Database.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: This class powers the SQLite database that is used to store products. This database is the foundation of the entire application. This class also contains 50 items that are added to
    the database and that come by default with the application. Users can always add additional items to this product database if they would like.
*/

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
        private UserListDetailed _selectedItem;

        public string username { get => username1; set => username1 = value; }
        public string scannerResult { get => result; set => result = value; }
        public UserListDetailed selectedItem { get => _selectedItem; set => _selectedItem = value; }

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            // For debug purposes
            //_database.DeleteAllAsync<UserListDetailed>().Wait();
            //_database.DropTableAsync<UserListDetailed>().Wait();
            // For debug purposes
            //_database.DeleteAllAsync<Barcode>().Wait();
            //_database.DropTableAsync<Barcode>().Wait();

            _database.CreateTableAsync<Barcode>().Wait(); //table creation
            _database.CreateTableAsync<UserData>().Wait();
            _database.CreateTableAsync<UserList>().Wait();
            _database.CreateTableAsync<UserListDetailed>().Wait();

            populateBarcodeTable();
        }

        public Task<List<UserListDetailed>> GetListDetailedAsync(string listID)
        {
            return _database.Table<UserListDetailed>().Where(items => items.listID == listID).ToListAsync();
        }

        public Task<List<UserList>> GetListAsync(string username)
        {
            return _database.Table<UserList>().Where(items => items.username == username).ToListAsync();
        }

        public Task<List<Barcode>> GetBarcodesAllAsync()
        {
            return _database.Table<Barcode>().ToListAsync();
        }

        public Task<List<Barcode>> GetBarcodesAsync(string barcodeNumber)
        {
            return _database.QueryAsync<Barcode>("Select * FROM Barcode WHERE ProductName LIKE ? OR SKU LIKE ?", "%" + barcodeNumber + "%", "%" + barcodeNumber + "%"); //SQLite query
        }

        public Task<Barcode> GetBarcodeAsync(string barcodeNumber)
        {
            return _database.Table<Barcode>().Where(items => items.SKU == barcodeNumber).FirstOrDefaultAsync();
        }

        public Task<List<Barcode>> GetBarcodesNameAsync(string productName)
        {
            return _database.QueryAsync<Barcode>("Select * FROM Barcode WHERE ProductName LIKE ?", "%" + productName + "%"); //SQLite query
        }

        public Task<int> SavePersonAsync(UserData userData)
        {
            return _database.InsertOrReplaceAsync(userData);
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

        public Task<int> DeleteListAsync(UserList userList)
        {
            _database.Table<UserListDetailed>().DeleteAsync(item => item.listID == userList.listID);
            return _database.DeleteAsync(userList);
        }

        public Task<int> DeleteListDetailedAsync(UserListDetailed userListDetailed)
        {
            return _database.DeleteAsync(userListDetailed);
        }

        private Task<int> populateBarcodeTable() //products that are already added into the database by default (users can always add additional products to this list)
        {
            _database.InsertOrReplaceAsync(new Barcode { SKU = "16571910303", ProductName = "Sparkling Ice Water - Black Raspberry", Price = 1 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "85227617524", ProductName = "#10 Envelopes - 40 count", Price = 1.64 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "681131234511", ProductName = "Spring Valley Vitamin D (5000 IU) - 400 Ct", Price = 11.88 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "748927024821", ProductName = "Optimum Nutrition ZMA, 90 Ct", Price = 17.94 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "78742279091", ProductName = "Great Value Purified Drinking Water, 40 Ct", Price = 3.98 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "33200000938", ProductName = "Aim Cavity Protection Toothpaste", Price = 0.85 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "78742371054", ProductName = "Great Value Quick Oats, 42 oz", Price = 2.7 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "33200352402", ProductName = "Scrub Free Soap Scum Remover, Lemon, 32 oz", Price = 2.08 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "37000397137", ProductName = "Dawn Ultra Dishwashing Liquid Dish Soap, 7 fl oz", Price = 3.3 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "44600015934", ProductName = "Clorox Disinfecting Wipes, Bleach Free", Price = 2.58 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "78742352060", ProductName = "Great Value Long Grain Enriched Rice, 80 oz", Price = 2.48 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "53891101738", ProductName = "FoodSaver FreshSaver 1-quart vacuum zipper bags, 18 Ct", Price = 7.98 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "41789002328", ProductName = "Maruchan Instant Lunch Beef Flavor Ramen Noodle, 12 pack", Price = 2.27 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "37000800682", ProductName = "Bounce Outdoor Fresh, 80 ct", Price = 3.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "24200050160", ProductName = "Purex Liquid Laundry Detergent, Mountain Breeze 150 fl oz", Price = 7.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "25500204215", ProductName = "Folgers Classic Roast Ground Coffee, 30.5 oz", Price = 6.92 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "78821200350", ProductName = "Nickles Honey Sandwich Buns, Country Style Bread", Price = 2.08 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "73209000317", ProductName = "Golding Farms Honey, 24 oz", Price = 5.78 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "817885000021", ProductName = "Slap Ya Mama Hot Blend, 8 oz", Price = 2.57 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "72310010581", ProductName = "Bigelow Tea Herbal Tea Plus Lemon Ginger, 18 bags", Price = 2.67 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "70734000108", ProductName = "Celestial Seasonings, Chamomile Herbal Tea Bags, 20 Ct", Price = 2.37 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "10900205184", ProductName = "Reynolds Kitch Slow Cooker Liners, regular size, 8 Ct", Price = 3.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "13700824340", ProductName = "Hefty Slider Freezer bags, Gallon Size, 34 Ct", Price = 4.78 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "829610003290", ProductName = "Roku Express 2", Price = 29 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "37000944792", ProductName = "Old Spice Sweat Defense Antiperspirant Deodorant, 2.6 oz", Price = 4.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "301871371122", ProductName = "Cerave Moisturizing Lotion, 12 oz", Price = 11.62 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "37000956648", ProductName = "Crest Scope Outlast Mouthwash, Long Lasting Mint, 500 mL", Price = 2.66 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "302993927169", ProductName = "Cetaphil Daily Facial Cleanser, 16 fl oz", Price = 9.52 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "858098007216", ProductName = "Cremo Barber Grade Hair Styling Pomade, Polish, 4 oz", Price = 9.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "17000209296", ProductName = "Dial Complete Antibacterial Liquid Hand Soap, Gold, 11 fl oz", Price = 1.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "681131039376", ProductName = "Equate Sensitive Skin Shave Foam, 11 oz", Price = 2.3 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "305215007005", ProductName = "Q Tips Original Cotton Swabs, 500 Ct", Price = 3.23 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "681131285681", ProductName = "Equate 3% Hydrogen Peroxide 32 oz", Price = 0.96 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "78742107677", ProductName = "Great Value Multipurpose Brush", Price = 2.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "52800676381", ProductName = "Neutrogena Tea Tree Oil Conditioner, 12 fl oz", Price = 8.74 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "52800676411", ProductName = "Neutrogena Lightweight Shampoo, 12 fl oz", Price = 8.74 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "89094022990", ProductName = "Isopure zero Carb Protein Powder, Unflavored, 1 lb", Price = 19.98 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "681131125345", ProductName = "Equate Vitamin C Drink mix, orange, 30 Ct", Price = 7.92 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "681131633284", ProductName = "Spring Valley Zinc Caplets, 50 mg, 200 Ct", Price = 3.96 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "631656712766", ProductName = "Purely Inspired Collagen Protein Powder, 1 lb", Price = 19.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "748927063875", ProductName = "Optimum Nutrition Gold Standard Isolate Protein, Chocolate, 1.13 lb", Price = 24.98 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "52000042276", ProductName = "Gatorade Zero Thirst Quencher, Lemon Lime, 32 oz", Price = 1.14 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "672935102472", ProductName = "Dr. Fresh Dailies Toothbrushes, firm, 6 Ct", Price = 1 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "681131078405", ProductName = "Equate Extra Strength Acetaminophen Caplets, 500mg, 200 Ct", Price = 3.22 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "300450581181", ProductName = "Sudafed PE Maximum Strength Non Drowsy Sinus Decongestant, 18 Ct", Price = 6.48 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "731124443007", ProductName = "Perform Pain Relieving Roll-on, 3 fl oz", Price = 6.95 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "019200771825", ProductName = "Lysol Disinfecting Wipes-Lemon/Lime Blosson Scent, 40ct.", Price = 5.99 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "030034936846", ProductName = "Giant Eagle Vinyl Powder-Free Medical Gloves", Price = 3.99 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "044000063047", ProductName = "Belvita Sandwich Crackers-Cinnamon Sugar", Price = 2.58 });
            return _database.InsertOrReplaceAsync(new Barcode { SKU = "78742351931", ProductName = "Great Value Spring Water, 1 Gallon", Price = 0.88 });
        }
    }
}