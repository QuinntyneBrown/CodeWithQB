using System;

namespace IntegrationTests.Features
{
    public class ShoppingCartItemScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string ShoppingCartItems = "api/shoppingCartItems";

            public static string ShoppingCartItemById(Guid id)
            {
                return $"api/shoppingCartItems/{id}";
            }

            public static string ShoppingCartById(Guid id)
            {
                return $"api/shoppingCarts/{id}";
            }
        }

        public static class Post
        {
            public static string ShoppingCartItems = "api/shoppingCartItems";
        }

        public static class Delete
        {
            public static string ShoppingCartItem(int id)
            {
                return $"api/shoppingCartItems/{id}";
            }
        }
    }
}
