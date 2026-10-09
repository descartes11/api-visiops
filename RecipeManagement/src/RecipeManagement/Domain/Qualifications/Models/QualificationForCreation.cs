namespace RecipeManagement.Domain.Qualifications.Models;

/// <summary>Modèle de domaine pour créer la fiche (passé à Qualification.Create). Le KcrId vient de l'URL.</summary>
public sealed record QualificationForCreation
{
    /// <summary>Exigences de recrutement minimales (minimum_recruitment_requirements).</summary>
    public string? MinimumRecruitmentRequirements { get; set; }

    /// <summary>Vérification des exigences de recrutement (recruitment_requirements_verification).</summary>
    public string? RecruitmentRequirementsVerification { get; set; }

    /// <summary>Seuils de performance requis (required_performance_thresholds).</summary>
    public string? RequiredPerformanceThresholds { get; set; }

    /// <summary>Commentaires (comments).</summary>
    public string? Comments { get; set; }

    /// <summary>Compétences et connaissances spécifiques (specific_skills_and_knowledge).</summary>
    public string? SpecificSkillsAndKnowledge { get; set; }

    /// <summary>Modules de formation (training_modules).</summary>
    public string? TrainingModules { get; set; }

    /// <summary>Mode de formation (présentiel, e-learning, mixte, sur poste…) (training_mode).</summary>
    public string? TrainingMode { get; set; }

    /// <summary>Personnel autorisé à délivrer la formation (authorized_training_personnel).</summary>
    public string? AuthorizedTrainingPersonnel { get; set; }

    /// <summary>Vérification des compétences minimales (minimum_competency_verification).</summary>
    public string? MinimumCompetencyVerification { get; set; }

    /// <summary>Seuil de performances pour la formation initiale (initial_training_performance_threshold).</summary>
    public string? InitialTrainingPerformanceThreshold { get; set; }

    /// <summary>Seuil de performances pour la revérification annuelle ou suite à un changement (recurrent_check_performance_threshold).</summary>
    public string? RecurrentCheckPerformanceThreshold { get; set; }

    /// <summary>Cadre ou méthodologie de formation (training_framework).</summary>
    public string? TrainingFramework { get; set; }
}
