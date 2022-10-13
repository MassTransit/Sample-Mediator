namespace Sample.ScopeApi.Filters;

public class WarehouseIdFilter
{
    readonly RequestDelegate _next;

    public WarehouseIdFilter(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        var warehouseId = context.Request.Headers["X-MyCo-WarehouseId"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(warehouseId))
        {
            context.Response.StatusCode = 400;
            return context.Response.WriteAsync("WarehouseId not found");
        }

        context.Items["WarehouseId"] = warehouseId;

        var tenantContext = context.RequestServices.GetRequiredService<WarehouseContext>();
        tenantContext.WarehouseId = warehouseId;

        return _next.Invoke(context);
    }
}