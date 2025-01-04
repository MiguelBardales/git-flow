using System;
using System.Collections.Generic;
using System.Text;


namespace DBEntity
{
    public class EntityCategories
    {
        public int CategoryID { get; set; } // MEJORA: Reemplazar por Key, ID - Alias BD
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
