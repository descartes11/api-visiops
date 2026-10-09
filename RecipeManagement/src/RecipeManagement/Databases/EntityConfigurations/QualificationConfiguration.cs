namespace RecipeManagement.Databases.EntityConfigurations;

using RecipeManagement.Domain.Qualifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class QualificationConfiguration : IEntityTypeConfiguration<Qualification>
{
    /// <summary>
    /// La configuration de la table kcr_qualification.
    /// </summary>
    public void Configure(EntityTypeBuilder<Qualification> builder)
    {
        builder.ToTable("kcr_qualification");

        builder.Property(x => x.Id).HasColumnName("kcr_qualification_id");

        builder.Property(x => x.KcrId)
            .HasColumnName("kcr_role_id")
            .IsRequired();

        builder.Property(x => x.MinimumRecruitmentRequirements)
            .HasColumnName("minimum_recruitment_requirements");
        builder.Property(x => x.RecruitmentRequirementsVerification)
            .HasColumnName("recruitment_requirements_verification");
        builder.Property(x => x.RequiredPerformanceThresholds)
            .HasColumnName("required_performance_thresholds");
        builder.Property(x => x.Comments)
            .HasColumnName("comments");
        builder.Property(x => x.SpecificSkillsAndKnowledge)
            .HasColumnName("specific_skills_and_knowledge");
        builder.Property(x => x.TrainingModules)
            .HasColumnName("training_modules");
        builder.Property(x => x.TrainingMode)
            .HasColumnName("training_mode");
        builder.Property(x => x.AuthorizedTrainingPersonnel)
            .HasColumnName("authorized_training_personnel");
        builder.Property(x => x.MinimumCompetencyVerification)
            .HasColumnName("minimum_competency_verification");
        builder.Property(x => x.InitialTrainingPerformanceThreshold)
            .HasColumnName("initial_training_performance_threshold");
        builder.Property(x => x.RecurrentCheckPerformanceThreshold)
            .HasColumnName("recurrent_check_performance_threshold");
        builder.Property(x => x.TrainingFramework)
            .HasColumnName("training_framework");

        // Relation 1 — 0..1 : un rôle possède au plus une fiche de qualification.
        // La clé étrangère est portée par Qualification (KcrId).
        builder.HasOne(x => x.Kcr)

            .WithOne(r => r.Qualification)
            .HasForeignKey<Qualification>(x => x.KcrId)
            .OnDelete(DeleteBehavior.Restrict);

        // Une seule fiche par rôle, parmi les fiches non supprimées (suppression logique).
        builder.HasIndex(x => x.KcrId)
            .IsUnique()
            .HasFilter("\"IsDeleted\" = false");
    }
}
