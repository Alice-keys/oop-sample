namespace ACME.OOP.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents a monetary amount with currency as a Value Object.
/// </summary>
public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }
/// <summary>
///  Creates a new instance of the <see cref="Money"/> Value Object.
/// </summary>
/// <param name="amount">the monetary amount</param>
/// <param name="currency">the currency code (ISO 4217 format)</param>
/// <exception cref="ArgumentException">Thrown when currency is null, empty, or not a valid 3-letter ISO code.</exception>
    public Money(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3) //código ISO de 3 letras
            throw new ArgumentException("Currency must be a valid 3-letter ISO code.", nameof(currency));
        Amount = amount;
        Currency = currency;
    }

/// <summary>
/// Returns a string representation of the monetary amount and currency.
/// </summary>
/// <returns></returns>
    public override string ToString() => $"{Amount} {Currency}"; //mostrar como cadena
}