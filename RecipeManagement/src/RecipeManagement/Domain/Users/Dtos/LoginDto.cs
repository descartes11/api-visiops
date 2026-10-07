namespace RecipeManagement.Domain.Users.Dtos;

using System.ComponentModel.DataAnnotations;

public sealed record LoginDto(
    [Required, EmailAddress] string Email,
    [Required] string Password);
