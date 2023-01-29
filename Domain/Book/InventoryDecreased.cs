using MediatR;

namespace Domain.Book
{
    public class InventoryDecreased : INotification
    {
        public InventoryDecreased(int bookId)
        {
            BookId = bookId;
        }

        public int BookId { get; }
    }
}