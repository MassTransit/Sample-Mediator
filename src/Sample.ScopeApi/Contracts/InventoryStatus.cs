namespace Sample.ScopeApi.Contracts;

public record InventoryStatus
{
    public string? WarehouseId { get; init; }

    public string? Sku { get; init; }

    public int OnHand { get; init; }
}