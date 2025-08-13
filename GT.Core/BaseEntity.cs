namespace GT.Core;

/// <summary>
/// Serves as a base class for entities with a unique identifier.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }
}
