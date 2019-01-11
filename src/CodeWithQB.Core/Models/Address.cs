using System;

namespace CodeWithQB.Core.Models
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
    }
    
}
