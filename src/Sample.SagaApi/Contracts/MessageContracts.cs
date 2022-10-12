using System.ComponentModel;
using System.Runtime.CompilerServices;
using MassTransit;

namespace Sample.SagaApi.Contracts;

public static class MessageContracts
{
    [ModuleInitializer]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Initialize()
    {
        MessageCorrelation.UseCorrelationId<SubmitOrder>(x => x.OrderId);
        MessageCorrelation.UseCorrelationId<GetOrderStatus>(x => x.OrderId);
        MessageCorrelation.UseCorrelationId<CancelOrder>(x => x.OrderId);
    }
}