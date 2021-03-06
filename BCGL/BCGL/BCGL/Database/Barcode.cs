﻿/*Barcode.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: The Barcode class represents a barcode object. Barcodes are found on every product and contain a SKU number, the product's name, and price. Every product contains a unique barcode.
*/

using SQLite;

namespace BCGL
{
    [Table("Barcode")] // Create idenitfier for table for when used in code
    public class Barcode
    {
        [Unique, PrimaryKey] // Make varible directly below unique and a primary key
        public string SKU { get; set; }
        public string ProductName { get; set; } 
        public double Price { get; set; }
    }
}