namespace RecipeManagement.Domain.Qualifications.DomainEvents;

/// <summary>Levé quand la fiche de qualification d'un rôle est créée.</summary>
public sealed class QualificationCreated : DomainEvent
{
    public Qualification Qualification { get; set; } = default!;
}
