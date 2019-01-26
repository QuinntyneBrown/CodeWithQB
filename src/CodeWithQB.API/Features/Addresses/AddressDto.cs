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
}
