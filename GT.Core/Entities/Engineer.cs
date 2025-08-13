namespace GT.Core.Entities;

/// <summary>
/// Represents an engineer entity with a name and an Arabic name.
/// Inherits from <see cref="BaseEntity"/>.
/// </summary>
public class Engineer : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Engineer"/> class.
    /// </summary>
    public Engineer()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Engineer"/> class with a specified ID.
    /// </summary>
    /// <param name="id">The unique identifier for the engineer.</param>
    public Engineer(Guid id)

    {
        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Engineer"/> class with a specified ID, name, and Arabic name.
    /// </summary>
    /// <param name="id">The unique identifier for the engineer.</param>
    /// <param name="name">The name of the engineer.</param>
    /// <param name="nameAR">The Arabic name of the engineer.</param>
    public Engineer(Guid id, string name, string nameAR)
    {
        Id = id;
        Name = name;
        NameAR = nameAR;
    }

    /// <summary>
    /// Gets or sets the name of the engineer.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the Arabic name of the engineer.
    /// </summary>
    public string NameAR { get; set; } = null!;
}