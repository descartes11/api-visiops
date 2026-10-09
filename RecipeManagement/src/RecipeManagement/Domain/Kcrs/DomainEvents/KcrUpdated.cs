namespace RecipeManagement.Domain.Kcrs.DomainEvents;

/// <summary>
/// Événement de domaine levé lorsqu'un rôle existant est modifié.
///
/// Il est ajouté par <c>QueueDomainEvent(...)</c> dans <see cref="Kcr.Update"/>
/// et publié par le DbContext après la sauvegarde.
/// </summary>
public sealed class KcrUpdated : DomainEvent
{
    /// <summary>
    /// Identifiant du rôle modifié.
    /// Un handler qui a besoin des données à jour peut relire
    /// le rôle depuis le repository avec cet Id.
    /// </summary>
    public Guid Id { get; set; }
}
