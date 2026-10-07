namespace RecipeManagement.Databases;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeManagement.Domain.Users;

public sealed class AuthDbContext(DbContextOptions<AuthDbContext> options)
    : IdentityDbContext<ApplicationUser>(options);
