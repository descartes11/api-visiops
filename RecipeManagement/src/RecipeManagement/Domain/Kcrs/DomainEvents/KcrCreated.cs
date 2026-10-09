namespace RecipeManagement.Domain.Kcrs.DomainEvents;

/// <summary>
/// Événement de domaine levé lorsqu'un nouveau rôle est créé.
///
/// Il est ajouté à la liste DomainEvents de l'entité par
/// <c>QueueDomainEvent(...)</c> dans <see cref="Kcr.Create"/>.
/// Le DbContext publie ensuite ces événements (via MediatR) après
/// la sauvegarde, ce qui permet de réagir à la création
/// (envoyer une notification, écrire un journal, etc.) sans
/// surcharger l'entité elle-même.
/// </summary>
public sealed class KcrCreated : DomainEvent
{
    /// <summary>
    /// Le rôle qui vient d'être créé.
    /// On transmet l'entité complète car l'Id seul ne suffit pas
    /// toujours aux gestionnaires (handlers) de l'événement.
    /// </summary>
    public Kcr Kcr { get; set; } = default!;
}
