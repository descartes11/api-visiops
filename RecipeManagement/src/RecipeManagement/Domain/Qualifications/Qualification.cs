namespace RecipeManagement.Domain.Qualifications;

using RecipeManagement.Domain.Qualifications.DomainEvents;
using RecipeManagement.Domain.Qualifications.Models;
using RecipeManagement.Domain.Kcrs;

/// <summary>
/// Fiche de qualification d'un rôle (remplace l'ancien champ Kcr.MinimumQualifications).
///
/// Relation : un Kcr possède au plus UNE Qualification (1 — 0..1).
/// La clé étrangère KcrId (kcr_role_id) se trouve ici, côté Qualification.
///
/// Hérite de BaseEntity : Id, CreatedOn/By, LastModifiedOn/By, IsDeleted, DomainEvents.
/// </summary>
public class Qualification : BaseEntity
{
    /// <summary>Clé étrangère vers KCR_ROLE (kcr_role_id).</summary>
    public Guid KcrId { get; private set; }

    /// <summary>Navigation vers le rôle propriétaire.</summary>
    public Kcr Kcr { get; private set; } = default!;

    // ---- Les 12 champs de la fiche (texte libre, tous optionnels) ----

    /// <summary>Exigences de recrutement minimales (minimum_recruitment_requirements).</summary>
    public string? MinimumRecruitmentRequirements { get; private set; }

    /// <summary>Vérification des exigences de recrutement (recruitment_requirements_verification).</summary>
    public string? RecruitmentRequirementsVerification { get; private set; }

    /// <summary>Seuils de performance requis (required_performance_thresholds).</summary>
    public string? RequiredPerformanceThresholds { get; private set; }

    /// <summary>Commentaires (comments).</summary>
    public string? Comments { get; private set; }

    /// <summary>Compétences et connaissances spécifiques (specific_skills_and_knowledge).</summary>
    public string? SpecificSkillsAndKnowledge { get; private set; }

    /// <summary>Modules de formation (training_modules).</summary>
    public string? TrainingModules { get; private set; }

    /// <summary>Mode de formation (présentiel, e-learning, mixte, sur poste…) (training_mode).</summary>
    public string? TrainingMode { get; private set; }

    /// <summary>Personnel autorisé à délivrer la formation (authorized_training_personnel).</summary>
    public string? AuthorizedTrainingPersonnel { get; private set; }

    /// <summary>Vérification des compétences minimales (minimum_competency_verification).</summary>
    public string? MinimumCompetencyVerification { get; private set; }

    /// <summary>Seuil de performances pour la formation initiale (initial_training_performance_threshold).</summary>
    public string? InitialTrainingPerformanceThreshold { get; private set; }

    /// <summary>Seuil de performances pour la revérification annuelle ou suite à un changement (recurrent_check_performance_threshold).</summary>
    public string? RecurrentCheckPerformanceThreshold { get; private set; }

    /// <summary>Cadre ou méthodologie de formation (training_framework).</summary>
    public string? TrainingFramework { get; private set; }

    /// <summary>
    /// Fabrique : crée la fiche de qualification d'un rôle.
    /// L'existence du rôle est vérifiée dans la feature AddQualification.
    /// </summary>
    public static Qualification Create(Guid kcrId, QualificationForCreation data)
    {
        if (kcrId == Guid.Empty)
            throw new ArgumentException("Le rôle est obligatoire.", nameof(kcrId));

        var q = new Qualification { KcrId = kcrId };
        q.MinimumRecruitmentRequirements = data.MinimumRecruitmentRequirements;
        q.RecruitmentRequirementsVerification = data.RecruitmentRequirementsVerification;
        q.RequiredPerformanceThresholds = data.RequiredPerformanceThresholds;
        q.Comments = data.Comments;
        q.SpecificSkillsAndKnowledge = data.SpecificSkillsAndKnowledge;
        q.TrainingModules = data.TrainingModules;
        q.TrainingMode = data.TrainingMode;
        q.AuthorizedTrainingPersonnel = data.AuthorizedTrainingPersonnel;
        q.MinimumCompetencyVerification = data.MinimumCompetencyVerification;
        q.InitialTrainingPerformanceThreshold = data.InitialTrainingPerformanceThreshold;
        q.RecurrentCheckPerformanceThreshold = data.RecurrentCheckPerformanceThreshold;
        q.TrainingFramework = data.TrainingFramework;

        q.QueueDomainEvent(new QualificationCreated { Qualification = q });
        return q;
    }

    /// <summary>Remplace toutes les valeurs de la fiche (PUT).</summary>
    public Qualification Update(QualificationForUpdate data)
    {
        MinimumRecruitmentRequirements = data.MinimumRecruitmentRequirements;
        RecruitmentRequirementsVerification = data.RecruitmentRequirementsVerification;
        RequiredPerformanceThresholds = data.RequiredPerformanceThresholds;
        Comments = data.Comments;
        SpecificSkillsAndKnowledge = data.SpecificSkillsAndKnowledge;
        TrainingModules = data.TrainingModules;
        TrainingMode = data.TrainingMode;
        AuthorizedTrainingPersonnel = data.AuthorizedTrainingPersonnel;
        MinimumCompetencyVerification = data.MinimumCompetencyVerification;
        InitialTrainingPerformanceThreshold = data.InitialTrainingPerformanceThreshold;
        RecurrentCheckPerformanceThreshold = data.RecurrentCheckPerformanceThreshold;
        TrainingFramework = data.TrainingFramework;

        QueueDomainEvent(new QualificationUpdated { Id = Id });
        return this;
    }

    /// <summary>Constructeur requis par EF Core. Utiliser Qualification.Create(...).</summary>
    protected Qualification() { }
}
