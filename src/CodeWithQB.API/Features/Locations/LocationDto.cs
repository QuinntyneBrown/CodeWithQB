using CodeWithQB.API.Features.Addresses;
using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Locations
{
    public class LocationDto
    {
        public Guid LocationId { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
    }
}
