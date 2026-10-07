namespace RecipeManagement.Domain.Users.Dtos;

using System.ComponentModel.DataAnnotations;

public sealed record RegisterDto(
    [Required, EmailAddress] string Email,
    [Required] string Password);
