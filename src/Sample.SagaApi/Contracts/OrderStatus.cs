namespace Sample.SagaApi.Contracts;

public record OrderStatus
{
    public Guid OrderId { get; init; }
    public string? Status { get; init; }
}