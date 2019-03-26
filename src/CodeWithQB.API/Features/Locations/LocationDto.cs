using CodeWithQB.Api.Features.Addresses;
using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.Api.Features.Locations
{
    public class LocationDto
    {
        public Guid LocationId { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
    }
}
