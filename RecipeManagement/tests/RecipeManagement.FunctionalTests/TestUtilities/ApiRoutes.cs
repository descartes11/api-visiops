namespace RecipeManagement.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

    public static class Recipes
    {
        public static string GetList(string version = "v1") => $"{Base}/{version}/recipes";
        public static string GetAll(string version = "v1") => $"{Base}/{version}/recipes/all";
        public static string GetRecord(Guid id, string version = "v1") => $"{Base}/{version}/recipes/{id}";
        public static string Delete(Guid id, string version = "v1") => $"{Base}/{version}/recipes/{id}";
        public static string Put(Guid id, string version = "v1") => $"{Base}/{version}/recipes/{id}";
        public static string Create(string version = "v1") => $"{Base}/{version}/recipes";
        public static string CreateBatch(string version = "v1") => $"{Base}/{version}/recipes/batch";
    }
}
