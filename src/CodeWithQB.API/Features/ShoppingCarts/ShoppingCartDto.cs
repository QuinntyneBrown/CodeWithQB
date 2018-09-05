using CodeWithQB.Core.Models;
using System;
using System.Collections.Generic;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class ShoppingCartDto
    {        
        public Guid ShoppingCartId { get; set; }
        public ICollection<Guid> ShoppingCartItemIds { get; set; }
        public static ShoppingCartDto FromShoppingCart(ShoppingCart shoppingCart)
            => new ShoppingCartDto
            {
                ShoppingCartId = shoppingCart.ShoppingCartId,
                ShoppingCartItemIds = shoppingCart.ShoppingCartItemIds                
            };
    }
}
