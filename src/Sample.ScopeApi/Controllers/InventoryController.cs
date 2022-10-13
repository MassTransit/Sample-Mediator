using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Sample.ScopeApi.Contracts;

namespace Sample.ScopeApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController :
    ControllerBase
{
    readonly ILogger<InventoryController> _logger;
    readonly IMediator _mediator;

    public InventoryController(ILogger<InventoryController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetInventoryStatus")]
    public Task<InventoryStatus> Get(string sku)
    {
        _logger.LogInformation("GetInventoryStatus: {Sku}", sku);

        return _mediator.SendRequest(new GetInventoryStatus { Sku = sku });
    }
}