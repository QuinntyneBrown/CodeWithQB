using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class ShoppingCartItemDto
    {        
        public Guid ShoppingCartItemId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }        
        public int Quantity { get; set; }
        
        public static ShoppingCartItemDto FromShoppingCartItem(ShoppingCartItem shoppingCartItem)
            => new ShoppingCartItemDto
            {
                ShoppingCartItemId = shoppingCartItem.ShoppingCartItemId
            };
    }
}
