namespace Domain.Exceptions
{
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException(int bookId) : base($"Book not found. BookId: {bookId}")
        {
        }
    }
}
