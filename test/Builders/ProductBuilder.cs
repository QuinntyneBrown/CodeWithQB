using CodeWithQB.Core.Models;
using System;
using System.Runtime.Serialization;

namespace Builders
{
    public class ProductBuilder
    {
        private Product _product = (Product)FormatterServices.GetUninitializedObject(typeof(Product));

        public ProductBuilder Id(Guid id)
        {
            _product.ProductId = id;
            return this;
        }

        public Product Build() {
            return _product;
        }

        public Product BuildWithTestValues() {
            return _product;
        }
    }
}
