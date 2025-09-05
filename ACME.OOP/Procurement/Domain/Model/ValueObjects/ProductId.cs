namespace ACME.OOP.Procurement.Domain.Model.ValueObjects;
/// <summary>
/// value object representing a Product Identifier.
/// </summary>
public record ProductId
{//documentar bien
    public Guid Id { get; init; } 
    /// <summary>
    /// creates a new instance of the <see cref="ProductId"/> Value Object.
    /// </summary>
    /// <param name="id">the unique identifier for the product</param>
    /// <exception cref="ArgumentException">Thrown when the provided Guid is empty.</exception>
    public ProductId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("ProductId cannot be empty.", nameof(id));
        Id = id;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static ProductId New() => new (Guid.NewGuid());
    
    public override string ToString() => Id.ToString();
}