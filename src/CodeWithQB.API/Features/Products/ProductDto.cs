using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.Api.Features.Products
{
    public class ProductDto
    {        
        public Guid ProductId { get; set; }
        public string Name { get; set; }

        public static ProductDto FromProduct(Product product)
            => new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name
            };
    }
}
