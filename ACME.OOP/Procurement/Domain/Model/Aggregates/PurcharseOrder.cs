namespace ACME.OOP.Procurement.Domain.Model.Aggregates;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;
using ACME.OOP.Procurement.Domain.Model.ValueObjects;
/// <summary>
/// Represents a Purchase Order aggregate in the Procurement bounded context.
/// </summary>
/// <param name="orderNumber">The unique order number for the purchase order.</param>
/// <param name="supplierId">The ,see cref="SupplierId"/> of the supplier from whom the order is placed.</param>
/// <param name="orderDate">The date when the order was placed.</param>
/// <param name="currency">The currency code (ISO 4217) for the order, e.g., "USD", "EUR". Must be a valid 3-character code.</param>

public class PurcharseOrder(string orderNumber, SupplierId supplierId, DateTime orderDate, string currency)
{
    private readonly List<PurcharseOrderItem> _items = new();
    
    public string OrderNumber { get; } = orderNumber ?? throw new ArgumentException(nameof(orderNumber));
    public SupplierId SupplierId { get; } = supplierId ?? throw new ArgumentException(nameof(supplierId));
    public DateTime OrderDate { get; } = orderDate;
    
    
    public string Currency { get; } = string.IsNullOrWhiteSpace(currency) || currency.Length != 3 
        ? throw new ArgumentException(nameof(currency)) 
        : currency;
    //coincidir con 3 caracteres del ISO 4217
    
    public IReadOnlyList<PurcharseOrderItem> Lines => _items.AsReadOnly();
    //copia de solo lectura
    
    //public void AddLine(PurcharseOrderItem line) error a nivel ddd, alguien mas que no es el aggregate root puede crear lineas
    /// <summary>
    /// Adds a new line item to the purchase order.
    /// </summary>
    /// <param name="productId"> The ,see cref="ProductId"/> of the product being ordered.</param>
    /// <param name="quantity"> The quantity of the product being ordered. Must be greater than zero.</param>
    /// <param name="unitPriceAmount"> The unit price amount of the product. Must be a positive value.</param>
    /// <exception cref="ArgumentException"> Thrown when any of the parameters are invalid.</exception>
    public void AddLine(ProductId productId, int quantity, decimal unitPriceAmount) // se pone decimal porque ya esta el tipo de Money
    {
        ArgumentNullException.ThrowIfNull(nameof(productId));//otra forma de validar nulos
        if (quantity <= 0) 
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        if (unitPriceAmount <= 0)
            throw new ArgumentException("Unit Price Amount must be greater than zero.", nameof(unitPriceAmount));
        
        var unitPrice = new Money(unitPriceAmount, Currency);
        var item = new PurcharseOrderItem(productId, quantity, unitPrice);
        _items.Add(item);
    }
    /// <summary>
    /// Calculates the total amount for the purchase order by summing the totals of all line items.
    /// </summary>
    /// <returns>A new <see cref="Money"/> instance representing the total amount for the purchase order.</returns>

    public Money CalculateOrderTotal()
    {
        var totalAmount = _items.Sum(item => item.CalculateItemTotal().Amount);
        return new Money(totalAmount, Currency);
    }
    
}