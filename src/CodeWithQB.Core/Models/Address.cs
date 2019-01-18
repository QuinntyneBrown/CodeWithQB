using System.Collections.Generic;

namespace CodeWithQB.Core.Models
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }        
        public string PostalCode { get; private set; }

        private Address() { }

        public Address(string street, string city, string province, string country, string postalCode)
        {
            Street = street;
            City = city;
            Province = province;            
            PostalCode = postalCode;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return Province;            
            yield return PostalCode;
        }
    }

}
