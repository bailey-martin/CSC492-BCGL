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

            //May have to comment or uncomment these 2 lines
            //_database.DeleteAllAsync<UserListDetailed>().Wait();
            //_database.DropTableAsync<UserListDetailed>().Wait();
            //End of may have to comment/uncomment section
            _database.DeleteAllAsync<Barcode>().Wait();
            _database.DropTableAsync<Barcode>().Wait();

            _database.CreateTableAsync<Barcode>().Wait();
            _database.CreateTableAsync<UserData>().Wait();
            _database.CreateTableAsync<UserList>().Wait();
            _database.CreateTableAsync<UserListDetailed>().Wait();

            populateBarcodeTable();
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

        private Task<int> populateBarcodeTable()
        {
            _database.InsertOrReplaceAsync(new Barcode { SKU = "1657191030", ProductName = "Sparkling Ice Water - Black Raspberry", Price = 1 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "8522761752", ProductName = "#10 Envelopes - 40 count", Price = 1.64 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "8113123451", ProductName = "Spring Valley Vitamin D (5000 IU) - 400 Ct", Price = 11.88 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "4892702482", ProductName = "Optimum Nutrition ZMA, 90 Ct", Price = 17.94 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "7874227909", ProductName = "Great Value Purified Drinking Water, 40 Ct", Price = 3.98 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "3320000093", ProductName = "Aim Cavity Protection Toothpaste", Price = 0.85 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "7874237105", ProductName = "Great Value Quick Oats, 42 oz", Price = 2.7 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "3320035240", ProductName = "Scrub Free Soap Scum Remover, Lemon, 32 oz", Price = 2.08 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "3700039713", ProductName = "Dawn Ultra Dishwashing Liquid Dish Soap, 7 fl oz", Price = 3.3 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "4460001594", ProductName = "Clorox Disinfecting Wipes, Bleach Free", Price = 2.58 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "7874235206", ProductName = "Great Value Long Grain Enriched Rice, 80 oz", Price = 2.48 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "5389110173", ProductName = "FoodSaver FreshSaver 1-quart vacuum zipper bags, 18 Ct", Price = 7.98 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "4178900232", ProductName = "Maruchan Instant Lunch Beef Flavor Ramen Noodle, 12 pack", Price = 2.27 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "3700080068", ProductName = "Bounce Outdoor Fresh, 80 ct", Price = 3.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "2420005016", ProductName = "Purex Liquid Laundry Detergent, Mountain Breeze 150 fl oz", Price = 7.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "2550020421", ProductName = "Folgers Classic Roast Ground Coffee, 30.5 oz", Price = 6.92 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "7882120035", ProductName = "Nickles Honey Sandwich Buns, Country Style Bread", Price = 2.08 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "7320900031", ProductName = "Golding Farms Honey, 24 oz", Price = 5.78 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "1788500002", ProductName = "Slap Ya Mama Hot Blend, 8 oz", Price = 2.57 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "7231001058", ProductName = "Bigelow Tea Herbal Tea Plus Lemon Ginger, 18 bags", Price = 2.67 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "7073400010", ProductName = "Celestial Seasonings, Chamomile Herbal Tea Bags, 20 Ct", Price = 2.37 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "1090020518", ProductName = "Reynolds Kitch Slow Cooker Liners, regular size, 8 Ct", Price = 3.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "1370082434", ProductName = "Hefty Slider Freezer bags, Gallon Size, 34 Ct", Price = 4.78 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "2961000329", ProductName = "Roku Express 2", Price = 29 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "3700094479", ProductName = "Old Spice Sweat Defense Antiperspirant Deodorant, 2.6 oz", Price = 4.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "187137112", ProductName = "Cerave Moisturizing Lotion, 12 oz", Price = 11.62 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "3700095664", ProductName = "Crest Scope Outlast Mouthwash, Long Lasting Mint, 500 mL", Price = 2.66 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "299392716", ProductName = "Cetaphil Daily Facial Cleanser, 16 fl oz", Price = 9.52 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "5809800721", ProductName = "Cremo Barber Grade Hair Styling Pomade, Polish, 4 oz", Price = 9.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "1700020929", ProductName = "Dial Complete Antibacterial Liquid Hand Soap, Gold, 11 fl oz", Price = 1.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "8113103937", ProductName = "Equate Sensitive Skin Shave Foam, 11 oz", Price = 2.3 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "521500700", ProductName = "Q Tips Original Cotton Swabs, 500 Ct", Price = 3.23 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "8113128568", ProductName = "Equate 3% Hydrogen Peroxide 32 oz", Price = 0.96 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "7874210767", ProductName = "Great Value Multipurpose Brush", Price = 2.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "5280067638", ProductName = "Neutrogena Tea Tree Oil Conditioner, 12 fl oz", Price = 8.74 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "5280067641", ProductName = "Neutrogena Lightweight Shampoo, 12 fl oz", Price = 8.74 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "8909402299", ProductName = "Isopure Sero Carb Protein Powder, Unflavored, 1 lb", Price = 19.98 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "8113112534", ProductName = "Equate Vitamin C Drinnk mix, orange, 30 Ct", Price = 7.92 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "8113163328", ProductName = "Spring Valley Zinc Caplets, 50 mg, 200 Ct", Price = 3.96 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "3165671276", ProductName = "Purely Inspired Collagen Protein Powder, 1 lb", Price = 19.97 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "4892706387", ProductName = "Optimum Nutrition Gold Standard Isolate Protein, Chocolate, 1.13 lb", Price = 24.98 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "5200004227", ProductName = "Gatorade Zero Thirst Quencher, Lemon Lime, 32 oz", Price = 1.14 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "7293510247", ProductName = "Dr. Fresh Dailies Toothbrushes, firm, 6 Ct", Price = 1 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "8113107840", ProductName = "Equate Extra Strength Acetaminophen Caplets, 500 Ct", Price = 3.22 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "45058118", ProductName = "Sudafed PE Maximum Strength Non Drowsy Sinus Decongestant, 18 Ct", Price = 6.48 });
            _database.InsertOrReplaceAsync(new Barcode { SKU = "3112444300", ProductName = "Perform Pain Relieving Roll-on, 3 fl oz", Price = 6.95 });
            return _database.InsertOrReplaceAsync(new Barcode { SKU = "7874235193", ProductName = "Great Value Spring Water, 1 Gallon", Price = 0.88 });
            
        }
    }
}