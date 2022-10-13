using MassTransit.Mediator;
using Sample.ScopeApi.Contracts;

namespace Sample.ScopeApi.Consumers;

public class InventoryConsumer :
    MediatorRequestHandler<GetInventoryStatus, InventoryStatus>
{
    readonly WarehouseContext _warehouseContext;

    public InventoryConsumer(WarehouseContext warehouseContext)
    {
        _warehouseContext = warehouseContext;
    }

    protected override Task<InventoryStatus> Handle(GetInventoryStatus request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new InventoryStatus
        {
            WarehouseId = _warehouseContext.WarehouseId,

            Sku = request.Sku,
            OnHand = Random.Shared.Next(1, 1000),
        });
    }
}