namespace RecipeManagement.Resources;

public class RecipeManagementOptions
{
    public const string SectionName = "RecipeManagement";
    
    public RabbitMqOptions RabbitMq { get; set; } = new RabbitMqOptions();
    public ConnectionStringOptions ConnectionStrings { get; set; } = new ConnectionStringOptions();
    public AuthOptions Auth { get; set; } = new AuthOptions();
    public JwtOptions Jwt { get; set; } = new JwtOptions();
    public string JaegerHost { get; set; } = String.Empty;
    
    public class RabbitMqOptions
    {
        public const string SectionName = $"{RecipeManagementOptions.SectionName}:RabbitMq";
        public const string HostKey = nameof(Host);
        public const string VirtualHostKey = nameof(VirtualHost);
        public const string UsernameKey = nameof(Username);
        public const string PasswordKey = nameof(Password);
        public const string PortKey = nameof(Port);

        public string Host { get; set; } = String.Empty;
        public string VirtualHost { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Port { get; set; } = String.Empty;
    }

    public class ConnectionStringOptions
    {
        public const string SectionName = $"{RecipeManagementOptions.SectionName}:ConnectionStrings";
        public const string RecipeManagementKey = nameof(RecipeManagement); 
            
        public string RecipeManagement { get; set; } = String.Empty;
    }
    
    
    public class AuthOptions
    {
        public const string SectionName = $"{RecipeManagementOptions.SectionName}:Auth";

        public string Audience { get; set; } = String.Empty;
        public string Authority { get; set; } = String.Empty;
        public string AuthorizationUrl { get; set; } = String.Empty;
        public string TokenUrl { get; set; } = String.Empty;
        public string ClientId { get; set; } = String.Empty;
        public string ClientSecret { get; set; } = String.Empty;
    }

    public class JwtOptions
    {
        public const string SectionName = $"{RecipeManagementOptions.SectionName}:Jwt";
        public const int MinKeyLength = 32;

        public string Key { get; set; } = String.Empty;
        public string Issuer { get; set; } = String.Empty;
        public string Audience { get; set; } = String.Empty;
        public int ExpirationMinutes { get; set; } = 60;
    }
}

public static class RecipeManagementOptionsExtensions
{
    public static RecipeManagementOptions GetRecipeManagementOptions(this IConfiguration configuration)
    {
        return configuration
            .GetSection(RecipeManagementOptions.SectionName)
            .Get<RecipeManagementOptions>();
    }
    
    public static RecipeManagementOptions.RabbitMqOptions GetRabbitMqOptions(this IConfiguration configuration)
    {
        return configuration
            .GetSection(RecipeManagementOptions.RabbitMqOptions.SectionName)
            .Get<RecipeManagementOptions.RabbitMqOptions>();
    }
    
    public static RecipeManagementOptions.ConnectionStringOptions GetConnectionStringOptions(this IConfiguration configuration)
    {
        return configuration
            .GetSection(RecipeManagementOptions.ConnectionStringOptions.SectionName)
            .Get<RecipeManagementOptions.ConnectionStringOptions>();
    }
    
    public static RecipeManagementOptions.AuthOptions GetAuthOptions(this IConfiguration configuration)
    {
        return configuration
            .GetSection(RecipeManagementOptions.AuthOptions.SectionName)
            .Get<RecipeManagementOptions.AuthOptions>();
    }

    public static RecipeManagementOptions.JwtOptions GetJwtOptions(this IConfiguration configuration)
    {
        return configuration
            .GetSection(RecipeManagementOptions.JwtOptions.SectionName)
            .Get<RecipeManagementOptions.JwtOptions>();
    }

    public static string GetJaegerHostValue(this IConfiguration configuration)
    {
        return configuration
            .GetSection(RecipeManagementOptions.SectionName)
            .GetSection(nameof(RecipeManagementOptions.JaegerHost)).Value;
    }
}