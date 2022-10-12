namespace Sample.SagaApi;

public class OrderCanceledException : 
    Exception
{
    public OrderCanceledException(string message)
        : base(message)
    {
    }
}