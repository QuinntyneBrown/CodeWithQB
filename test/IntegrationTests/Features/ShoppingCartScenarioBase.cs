namespace IntegrationTests.Features
{
    public class ShoppingCartScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string ShoppingCarts = "api/shoppingCarts";

            public static string ShoppingCartById(int id)
            {
                return $"api/shoppingCarts/{id}";
            }
        }

        public static class Post
        {
            public static string ShoppingCarts = "api/shoppingCarts";
        }

        public static class Delete
        {
            public static string ShoppingCart(int id)
            {
                return $"api/shoppingCarts/{id}";
            }
        }
    }
}
