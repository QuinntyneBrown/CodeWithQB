namespace IntegrationTests.Features
{
    public class AddressScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string Addresses = "api/addresses";

            public static string AddressById(int id)
            {
                return $"api/addresses/{id}";
            }
        }

        public static class Post
        {
            public static string Addresses = "api/addresses";
        }

        public static class Delete
        {
            public static string Address(int id)
            {
                return $"api/addresses/{id}";
            }
        }
    }
}
