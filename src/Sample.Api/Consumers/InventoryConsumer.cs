using MassTransit.Mediator;
using Sample.Api.Contracts;

namespace Sample.Api.Consumers;

public class InventoryConsumer :
    MediatorRequestHandler<GetInventoryStatus, InventoryStatus>
{
    protected override Task<InventoryStatus> Handle(GetInventoryStatus request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new InventoryStatus
        {
            Sku = request.Sku,
            OnHand = Random.Shared.Next(1, 1000)
        });
    }
}