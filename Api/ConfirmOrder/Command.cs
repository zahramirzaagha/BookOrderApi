using MediatR;

namespace Api.ConfirmOrder
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