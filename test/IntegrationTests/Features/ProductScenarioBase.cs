namespace IntegrationTests.Features
{
    public class ProductScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string Products = "api/products";

            public static string ProductById(int id)
            {
                return $"api/products/{id}";
            }
        }

        public static class Post
        {
            public static string Products = "api/products";
        }

        public static class Delete
        {
            public static string Product(int id)
            {
                return $"api/products/{id}";
            }
        }
    }
}
