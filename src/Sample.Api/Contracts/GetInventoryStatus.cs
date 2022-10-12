using MassTransit.Mediator;

namespace Sample.Api.Contracts;

public record GetInventoryStatus :
    Request<InventoryStatus>
{
    public string? Sku { get; init; }
}