using MassTransit.Mediator;

namespace Sample.SagaApi.Contracts;

public record SubmitOrder :
    Request<OrderStatus>
{
    public Guid OrderId { get; set; }
}