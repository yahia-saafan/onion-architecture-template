namespace GT.Core.Interfaces;

/// <summary>
/// Defines a contract for entities that support soft deletion.
/// </summary>
public interface ISoftDeletedEntity
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is soft-deleted.
    /// </summary>
    bool Deleted { get; set; }
}
