using MediatR;

namespace Domain.Book
{
    internal class InventoryIncreased : INotification
    {
        public InventoryIncreased(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; }
    }
}