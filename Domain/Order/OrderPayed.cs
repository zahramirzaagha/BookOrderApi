using MediatR;

namespace Domain.Order
{
    public class OrderPayed : INotification
    {
        public OrderPayed(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}