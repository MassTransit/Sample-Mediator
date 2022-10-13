using MassTransit.Mediator;

namespace Sample.ScopeApi.Contracts;

public record GetInventoryStatus :
    Request<InventoryStatus>
{
    public string? Sku { get; init; }
}