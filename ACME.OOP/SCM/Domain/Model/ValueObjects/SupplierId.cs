namespace ACME.OOP.SCM.Domain.Model.ValueObjects;
/// <summary>
/// Value Object representing a Supplier Identifier.
/// </summary>
/// ampliar documentacion remarks
public record SupplierId
{
    public string Identifier { get; init; }

    /// <summary>
    /// Creates a new instance of the <see cref="SupplierId"/> Value Object.
    /// </summary>
    /// <param name="identifier"></param>
    /// <exception cref="ArgumentException"></exception>
    public SupplierId(string identifier)
    {
        if (string.IsNullOrEmpty(identifier))
            throw new ArgumentException("Identifier cannot be null or empty.", nameof(identifier));
        Identifier = identifier;
    }
}