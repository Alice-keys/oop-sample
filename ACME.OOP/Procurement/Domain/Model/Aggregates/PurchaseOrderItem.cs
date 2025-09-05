using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;
/// <summary>
/// Represents an item in a Purchase Order aggregate.
/// </summary>
/// <param name="productId">The ,see cref="ProductId"/> of the product being ordered.</param>
/// <param name="quantity">The quantity of the product being ordered. Must be greater than zero.</param>
/// <param name="unitPrice">The ,see cref="Money"/> representing the unit price of the product. Must be a positive amount.</param>
///
/// <exception cref="ArgumentException">Thrown when any of the parameters are invalid.</exception>
/// <exception cref="InvalidOperationException">Thrown when attempting to calculate the total for an item with invalid state.</exception>
public class PurchaseOrderItem(ProductId productId, int quantity, Money unitPrice)
{
    public ProductId ProductId { get; } = productId ?? throw new ArgumentException(nameof(ProductId));
    public int Quantity { get; } = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
    public Money UnitPrice { get; } = unitPrice ?? throw new ArgumentException(nameof(unitPrice));
    
    //ddd comienza con calculo de totales
    /// <summary>
    /// Calculates the total price for this purchase order item (UnitPrice * Quantity).
    /// </summary>
    /// <returns>A new <see cref="Money"/> instance representing the total price for this item.</returns>
    public Money CalculateItemTotal() => new(UnitPrice.Amount * Quantity, UnitPrice.Currency);
}