using MediatR;

namespace Domain.Order
{
    public class OrderScheduled : INotification
    {
        public OrderScheduled(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}