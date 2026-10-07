namespace RecipeManagement.Extensions.Services;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RecipeManagement.Databases;
using RecipeManagement.Domain.Users;
using Resources;

public static class AuthServiceExtension
{
    public static void AddAuth(this IServiceCollection services, string connectionString, IConfiguration configuration)
    {
        // separate history table so this context's migrations don't mix with RecipesDbContext's
        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(connectionString,
                builder => builder
                    .MigrationsAssembly(typeof(RecipesDbContext).Assembly.FullName)
                    .MigrationsHistoryTable("__EFMigrationsHistory_Auth")));

        services.AddHostedService<MigrationHostedService<AuthDbContext>>();

        services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AuthDbContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // evaluated lazily, so configuration overrides (env vars, tests) are picked up
                var jwt = configuration.GetJwtOptions();
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt?.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwt?.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };

                // without a valid key no token can validate, so every request stays 401
                if (jwt is not null && Encoding.UTF8.GetByteCount(jwt.Key) >= RecipeManagementOptions.JwtOptions.MinKeyLength)
                {
                    parameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                }

                options.TokenValidationParameters = parameters;
            });

        services.AddAuthorization();
    }
}
