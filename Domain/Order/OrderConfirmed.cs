using MediatR;

namespace Domain.Order
{
    public class OrderConfirmed : INotification
    {
        public OrderConfirmed(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}