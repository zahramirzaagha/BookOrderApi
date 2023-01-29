using MediatR;

namespace Api.ShipOrder
{
    public class Command : IRequest
    {
        public Command(int orderId, DateTimeOffset date)
        {
            OrderId = orderId;
            Date = date;
        }

        public DateTimeOffset Date { get; }
        
        public int OrderId { get; }
    }
}