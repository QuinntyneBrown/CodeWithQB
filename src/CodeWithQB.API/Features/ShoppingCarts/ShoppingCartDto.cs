using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class ShoppingCartDto
    {        
        public Guid ShoppingCartId { get; set; }
        public string Name { get; set; }

        public static ShoppingCartDto FromShoppingCart(ShoppingCart shoppingCart)
            => new ShoppingCartDto
            {
                ShoppingCartId = shoppingCart.ShoppingCartId,
                Name = shoppingCart.Name
            };
    }
}
