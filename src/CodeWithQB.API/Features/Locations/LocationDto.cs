using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Locations
{
    public class LocationDto
    {
        public Guid LocationId { get; set; }
        public string Name { get; set; }
        public static LocationDto FromLocation(Location location)
            => new LocationDto
            {
                LocationId = location.LocationId,
                Name = location.Name
            };
    }
}
