namespace Domain.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(int orderId) : base($"Order not found. OrderId: {orderId}")
        {
        }
    }
}