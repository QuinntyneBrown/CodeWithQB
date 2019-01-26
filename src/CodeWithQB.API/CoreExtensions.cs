using CodeWithQB.API.Features.Addresses;
using CodeWithQB.Core.Models;

namespace CodeWithQB.API
{
    public static class CoreExtensions
    {
        public static AddressDto ToDto(this Address src)
            => new AddressDto
            {
                Street = src.Street,
                City = src.City,
                Province = src.Province,
                PostalCode = src.PostalCode
            };
    }
}
