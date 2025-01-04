using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntityEmployee
    {
        public int EmployeeID { get; set; } // MEJORA: Reemplazar por Key, ID - Alias BD
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public string BirthDate { get; set; }
        public string HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
    }
}
