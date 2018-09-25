using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Addresses
{
    public class AddressDto
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }

        public static AddressDto FromAddress(Address address)
            => new AddressDto
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                Province = address.Province,
                PostalCode = address.PostalCode
            };
    }
}
