namespace Sample.Api.Contracts;

public record InventoryStatus
{
    public string? Sku { get; init; }

    public int OnHand { get; init; }
}