namespace RecipeManagement.Domain.Users.Dtos;

public sealed record TokenDto(string AccessToken, string TokenType, DateTimeOffset ExpiresAtUtc);
