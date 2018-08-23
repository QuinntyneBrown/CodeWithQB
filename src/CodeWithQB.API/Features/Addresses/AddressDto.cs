using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Addresses
{
    public class AddressDto
    {        
        public Guid AddressId { get; set; }
        public string Name { get; set; }

        public static AddressDto FromAddress(Address address)
            => new AddressDto
            {
                AddressId = address.AddressId,
                Name = address.Name
            };
    }
}
