using MediatR;

namespace Domain.Order
{
    public class OrderShipped : INotification
    {
        public OrderShipped(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}