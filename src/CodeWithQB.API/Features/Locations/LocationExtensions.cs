using CodeWithQB.Api.Features.Addresses;
using CodeWithQB.Core.Models;

namespace CodeWithQB.Api.Features.Locations
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
