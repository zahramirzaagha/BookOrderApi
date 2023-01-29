using MediatR;

namespace Api.AddInventory
{
    public class Command : IRequest
    {
        public Command(int bookId, int inventory)
        {
            BookId = bookId;
            Inventory = inventory;
        }

        public int BookId { get; }

        public int Inventory { get; }
    }
}
