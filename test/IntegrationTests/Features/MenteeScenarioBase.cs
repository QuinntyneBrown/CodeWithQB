namespace IntegrationTests.Features
{
    public class MenteeScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string Mentees = "api/mentees";

            public static string MenteeById(int id)
            {
                return $"api/mentees/{id}";
            }
        }

        public static class Post
        {
            public static string Mentees = "api/mentees";
            public static string Register = "api/mentees/register";
        }

        public static class Delete
        {
            public static string Mentee(int id)
            {
                return $"api/mentees/{id}";
            }
        }
    }
}
