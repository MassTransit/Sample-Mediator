using MassTransit.Mediator;

namespace Sample.SagaApi.Contracts;

public record GetOrderStatus :
    Request<OrderStatus>
{
    public Guid OrderId { get; set; }
}