using MassTransit;

namespace Sample.SagaApi.StateMachines;

public class OrderState :
    SagaStateMachineInstance
{
    public int CurrentState { get; set; }

    public Guid CorrelationId { get; set; }
}