using System;

namespace IntegrationTests.Features
{
    public class DashboardScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string Dashboards = "api/dashboards";

            public static string DashboardById(Guid id)
            {
                return $"api/dashboards/{id}";
            }
        }

        public static class Post
        {
            public static string Dashboards = "api/dashboards";
        }

        public static class Delete
        {
            public static string Dashboard(int id)
            {
                return $"api/dashboards/{id}";
            }
        }
    }
}
