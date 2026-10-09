namespace RecipeManagement.Domain.Qualifications.DomainEvents;

/// <summary>Levé quand la fiche de qualification d'un rôle est modifiée.</summary>
public sealed class QualificationUpdated : DomainEvent
{
    public Guid Id { get; set; }
}
