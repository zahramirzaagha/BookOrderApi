using MediatR;

namespace Api.PayOrder
{
    public class Command : IRequest
    {
        public Command(int orderId, decimal amount)
        {
            OrderId = orderId;
            Amount = amount;
        }

        public int OrderId { get; }
        
        public decimal Amount { get; }
    }
}
