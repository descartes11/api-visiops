namespace RecipeManagement.Extensions.Services;

using RecipeManagement.Services;
using Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Collections.Generic;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class SwaggerServiceExtension
{
    public static void AddSwaggerExtension(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var authOptions = configuration.GetAuthOptions();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(config =>
        {
            config.CustomSchemaIds(type => type.ToString().Replace("+", "."));
            config.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date"
            });

            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Paste the accessToken returned by POST /api/v1/auth/login."
            });
            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()
                }
            });

            config.IncludeXmlComments(string.Format(@$"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}RecipeManagement.WebApi.xml"));
        });
    }
}

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = "",
                Description = ""
            });
        }
    }
}
