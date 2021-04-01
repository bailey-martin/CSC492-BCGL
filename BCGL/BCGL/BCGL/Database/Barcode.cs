using SQLite;

namespace BCGL
{
    [Table("Barcode")]
    public class Barcode
    {
        [Unique, PrimaryKey]
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}