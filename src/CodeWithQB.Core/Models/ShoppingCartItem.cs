using CodeWithQB.Core.Framework;
using System;

namespace CodeWithQB.Core.Models
{
    public class ShoppingCartItem: Value<ShoppingCartItem>
    {
        public ShoppingCartItem(Guid shoppingCartId, Guid productId, int quantity)
        {
            ShoppingCartId = shoppingCartId;
            ProductId = productId;
            Quantity = quantity;
        }
        
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }        
    }
}
