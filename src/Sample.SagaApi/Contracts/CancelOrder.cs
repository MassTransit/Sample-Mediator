using MassTransit.Mediator;

namespace Sample.SagaApi.Contracts;

public record CancelOrder :
    Request<OrderStatus>
{
    public Guid OrderId { get; set; }
}