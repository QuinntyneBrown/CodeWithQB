namespace IntegrationTests.Features
{
    public class EventScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string Events = "api/events";

            public static string EventById(int id)
            {
                return $"api/events/{id}";
            }
        }

        public static class Post
        {
            public static string Events = "api/events";
        }

        public static class Delete
        {
            public static string Event(int id)
            {
                return $"api/events/{id}";
            }
        }
    }
}
