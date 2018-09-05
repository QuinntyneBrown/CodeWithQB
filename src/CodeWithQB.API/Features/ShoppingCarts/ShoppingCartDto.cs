using CodeWithQB.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class ShoppingCartDto
    {        
        public Guid ShoppingCartId { get; set; }
        public ICollection<ShoppingCartItemDto> ShoppingCartItems { get; set; }
        public static ShoppingCartDto FromShoppingCart(ShoppingCart shoppingCart)
            => new ShoppingCartDto
            {
                ShoppingCartId = shoppingCart.ShoppingCartId,
                ShoppingCartItems = shoppingCart.ShoppingCartItems.Select(x => ShoppingCartItemDto.FromModel(x)).ToList()              
            };
    }

    public class ShoppingCartItemDto {
        public Guid ProductId { get; set; }

        public static ShoppingCartItemDto FromModel(ShoppingCartItem model)
            => new ShoppingCartItemDto() { ProductId = model.ProductId };
    }
}
