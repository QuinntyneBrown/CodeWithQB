using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Products
{
    public class ProductDto
    {        
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public static ProductDto FromProduct(Product product)
            => new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Decription
            };
    }
}
