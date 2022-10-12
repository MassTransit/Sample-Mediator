using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Sample.SagaApi.Contracts;

namespace Sample.SagaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController :
    ControllerBase
{
    readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "SubmitOrder")]
    public Task<OrderStatus> Post(Guid orderId)
    {
        return _mediator.SendRequest(new SubmitOrder { OrderId = orderId });
    }

    [HttpGet(Name = "GetOrderStatus")]
    public Task<OrderStatus> Get(Guid orderId)
    {
        return _mediator.SendRequest(new GetOrderStatus { OrderId = orderId });
    }

    [HttpDelete(Name = "CancelOrder")]
    public Task<OrderStatus> Delete(Guid orderId)
    {
        return _mediator.SendRequest(new CancelOrder { OrderId = orderId });
    }
}