using MassTransit;

namespace Sample.ScopeApi.Filters;

public class HttpContextScopeFilter :
    IFilter<PublishContext>,
    IFilter<SendContext>,
    IFilter<ConsumeContext>
{
    readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextScopeFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task Send(ConsumeContext context, IPipe<ConsumeContext> next)
    {
        AddPayload(context);
        return next.Send(context);
    }

    public Task Send(PublishContext context, IPipe<PublishContext> next)
    {
        AddPayload(context);
        return next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }

    public Task Send(SendContext context, IPipe<SendContext> next)
    {
        AddPayload(context);
        return next.Send(context);
    }

    void AddPayload(PipeContext context)
    {
        if (_httpContextAccessor.HttpContext == null)
            return;

        var serviceProvider = _httpContextAccessor.HttpContext.RequestServices;
        context.GetOrAddPayload(() => serviceProvider);
        context.GetOrAddPayload<IServiceScope>(() => new NoopScope(serviceProvider));
    }

    class NoopScope :
        IServiceScope
    {
        public NoopScope(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public void Dispose()
        {
        }

        public IServiceProvider ServiceProvider { get; }
    }
}