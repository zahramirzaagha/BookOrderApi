using MediatR;

namespace Api.CancelOrder
{
    public class Command : IRequest
    {
        public Command(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}