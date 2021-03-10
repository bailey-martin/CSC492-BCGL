using SQLite;

namespace BCGL
{
    public class Barcode
    {
        [Unique]
        public int ID { get; set; }
        [Unique]
        public string Name { get; set; }
        [Unique]
        public int Age { get; set; }
    }
}