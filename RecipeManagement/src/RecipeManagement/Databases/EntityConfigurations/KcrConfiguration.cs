namespace RecipeManagement.Databases.EntityConfigurations;

using RecipeManagement.Domain.Kcrs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class KcrConfiguration : IEntityTypeConfiguration<Kcr>
{
    /// <summary>
    /// La configuration de la table kcr_role.
    /// </summary>
    public void Configure(EntityTypeBuilder<Kcr> builder)
    {
        builder.ToTable("kcr_role");

        builder.Property(x => x.Id).HasColumnName("kcr_role_id");

        builder.Property(x => x.KcrName)
            .HasColumnName("role_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description");


        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);

        // Nom unique, mais seulement parmi les rôles non supprimés (suppression logique),
        // pour pouvoir recréer un rôle dont l'ancien homonyme a été supprimé.
        builder.HasIndex(x => x.KcrName)
            .IsUnique()
            .HasFilter("\"IsDeleted\" = false");
    }
}
