using CodeWithQB.Core.Models;

namespace CodeWithQB.API.Features.Addresses
{
    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
    }

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
