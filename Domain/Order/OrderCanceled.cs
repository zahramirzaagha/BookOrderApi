using MediatR;

namespace Domain.Order
{
    public class OrderCanceled : INotification
    {
        public OrderCanceled(int orderId, OrderStatus orderStatusWhenCanceling)
        {
            OrderId = orderId;
            OrderStatusWhenCanceling = orderStatusWhenCanceling;
        }

        public int OrderId { get; }
        public OrderStatus OrderStatusWhenCanceling { get; }
    }
}