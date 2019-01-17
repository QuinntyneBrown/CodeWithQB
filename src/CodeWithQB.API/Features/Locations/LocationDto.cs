using System;

namespace CodeWithQB.API.Features.Locations
{
    public class LocationDto
    {
        public Guid LocationId { get; set; }
        public string Name { get; set; }
        public static LocationDto FromCustomer(LocationDto location)
            => new LocationDto
            {
                LocationId = location.LocationId,
                Name = location.Name
            };
    }
}
