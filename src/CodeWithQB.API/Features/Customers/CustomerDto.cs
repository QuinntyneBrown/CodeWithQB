using CodeWithQB.API.Features.Addresses;
using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Customers
{
    public class CustomerDto
    {        
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressDto Address { get; set; }

        public static CustomerDto FromCustomer(Customer customer)
            => new CustomerDto
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = AddressDto.FromAddress(customer.Address)
            };
    }
}
