namespace RecipeManagement.Domain.Visibilities;

using Ardalis.SmartEnum;
using RecipeManagement.Exceptions;

public sealed class Visibility : ValueObject
{
    private VisibilityEnum _visibility;
    public string Value
    {
        get => _visibility.Name;
        private set
        {
            if (!VisibilityEnum.TryFromName(value, true, out var parsed))
                throw new ValidationException($"Invalid Visibility. Please use one of the following: {string.Join(", ", ListNames())}");

            _visibility = parsed;
        }
    }
    
    public Visibility(string value)
    {
        Value = value;
    }

    public static Visibility Of(string value) => new Visibility(value);
    public static implicit operator string(Visibility value) => value.Value;
    public static List<string> ListNames() => VisibilityEnum.List.Select(x => x.Name).ToList();

   public static Visibility Public() => new Visibility(VisibilityEnum.Public.Name);
   public static Visibility FriendsOnly() => new Visibility(VisibilityEnum.FriendsOnly.Name);
   public static Visibility Private() => new Visibility(VisibilityEnum.Private.Name);

    private Visibility() { } // EF Core

    private abstract class VisibilityEnum(string name, int value)
        : SmartEnum<VisibilityEnum>(name, value)
    {
        public static readonly VisibilityEnum Public = new PublicType();
        public static readonly VisibilityEnum FriendsOnly = new FriendsOnlyType();
        public static readonly VisibilityEnum Private = new PrivateType();

        private class PublicType() : VisibilityEnum("Public", 0);

        private class FriendsOnlyType() : VisibilityEnum("Friends Only", 1);

        private class PrivateType() : VisibilityEnum("Private", 2);
    }
}