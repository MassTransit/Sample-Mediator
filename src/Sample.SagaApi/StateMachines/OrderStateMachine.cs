using MassTransit;
using MassTransit.Mediator;
using Sample.SagaApi.Contracts;

namespace Sample.SagaApi.StateMachines;

public class OrderStateMachine :
    MassTransitStateMachine<OrderState>
{
#pragma warning disable CS8618
    public OrderStateMachine()
#pragma warning restore CS8618
    {
        InstanceState(x => x.CurrentState, Submitted, Canceled);

        Initially(
            When(SubmitOrder)
                .TransitionTo(Submitted)
                .RespondWithOrderStatus()
        );

        DuringAny(
            When(OrderStatusRequested)
                .RespondWithOrderStatus()
        );

        During(Submitted,
            When(OrderCancellationRequested)
                .TransitionTo(Canceled)
                .RespondWithOrderStatus()
        );

        During(Canceled,
            When(OrderCancellationRequested)
                .RespondWithOrderStatus()
        );

        During(Submitted, Canceled,
            When(SubmitOrder)
                .RespondWithOrderStatus()
        );
    }

    //
    // ReSharper disable UnassignedGetOnlyAutoProperty
    // ReSharper disable MemberCanBePrivate.Global
    public State Submitted { get; }
    public State Canceled { get; }

    public Event<SubmitOrder> SubmitOrder { get; }
    public Event<GetOrderStatus> OrderStatusRequested { get; }
    public Event<CancelOrder> OrderCancellationRequested { get; }
}

public static class OrderStateMachineExtensions
{
    /// <summary>
    /// Extension methods make it easy to add the same activity to multiple state/event behaviors
    /// </summary>
    /// <param name="binder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static EventActivityBinder<OrderState, T> RespondWithOrderStatus<T>(this EventActivityBinder<OrderState, T> binder)
        where T : class, Request<OrderStatus>
    {
        return binder.RespondAsync(context => context.Init<OrderStatus>(new
        {
            OrderId = context.Saga.CorrelationId,
            Status = context.StateMachine.Accessor.Get(context)
        }));
    }

    public static EventActivityBinder<OrderState, T> ThrowOrderCanceledException<T>(this EventActivityBinder<OrderState, T> binder)
        where T : class
    {
        return binder.Then(_ => throw new OrderCanceledException("The order was canceled"));
    }
}