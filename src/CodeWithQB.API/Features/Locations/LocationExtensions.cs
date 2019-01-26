using CodeWithQB.API.Features.Addresses;
using CodeWithQB.Core.Models;

namespace CodeWithQB.API.Features.Locations
{
    public static class LocationExtensions
    {
        public static LocationDto ToDto(this Location x)
            => new LocationDto
            {
                LocationId = x.LocationId,
                Name = x.Name,
                Address = x.Address?.ToDto()
            };
    }
}
