using MediatR;

namespace Api.CreateOrder
{
    public class Command : IRequest<int>
    {
        public Command(int accountId, IEnumerable<OrderItem>? items)
        {
            AccountId = accountId;
            OrderItems = items;
        }

        public int AccountId { get; }

        public IEnumerable<OrderItem>? OrderItems { get; }
    }

    public class OrderItem
    {
        public OrderItem(int bookId, int quantity)
        {
            BookId = bookId;
            Quantity = quantity;
        }

        public int BookId { get; }

        public int Quantity { get; }
    }
}
