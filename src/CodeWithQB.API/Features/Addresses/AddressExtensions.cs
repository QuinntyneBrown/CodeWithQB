using CodeWithQB.API.Features.Addresses;
using CodeWithQB.Core.Models;

namespace CodeWithQB.API.Features.Addresses
{
    public static class AddressExtensions
    {
        public static AddressDto ToDto(this Address x)
            => new AddressDto
            {
                Street = x.Street,
                City = x.City,
                Province = x.Province,
                PostalCode = x.PostalCode
            };
    }
}
