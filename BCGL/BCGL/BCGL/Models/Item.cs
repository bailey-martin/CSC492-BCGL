/*Item.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: The Item class is built by Xamarin and used as a data model to store a list Id, Text, and Description.
  The item class is designed as a data model to represent every item on a shopping list. Every Item object contains an Id (unique identifier), as well as text and description.
*/

using System;

namespace BCGL.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public static explicit operator Item(UserList v)
        {
            throw new NotImplementedException();
        }
    }
}