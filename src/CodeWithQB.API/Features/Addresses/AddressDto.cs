using CodeWithQB.Core.Models;

namespace CodeWithQB.API.Features.Addresses
{
    public class AddressDto
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }
        public string PostalCode { get; private set; }

        public static AddressDto FromAddress(Address address)
            => new AddressDto
            {
                Street = address.Street,
                City = address.City,
                Province = address.Province,
                PostalCode = address.PostalCode
            };
    }
}
