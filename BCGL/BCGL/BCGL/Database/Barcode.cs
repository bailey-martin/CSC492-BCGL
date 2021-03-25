using SQLite;

namespace BCGL
{
    [Table("Barcode")]
    public class Barcode
    {
        [Unique, PrimaryKey]
        public int SKU { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}