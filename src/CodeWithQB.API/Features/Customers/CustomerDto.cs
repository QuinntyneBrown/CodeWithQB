using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.Api.Features.Customers
{
    public class CustomerDto
    {        
        public Guid CustomerId { get; set; }
        public string Name { get; set; }        
        public static CustomerDto FromCustomer(Customer customer)
            => new CustomerDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name
            };
    }
}
