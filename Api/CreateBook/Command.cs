using Domain.Book;
using MediatR;

namespace Api.CreateBook
{
    public class Command : IRequest<int>
    {
        public Command(string? isbn, string? title, BookCategory category, int publisherId, int yearPublished, int edition, decimal unitPrice, List<int>? authorIds)
        {
            Isbn = isbn;
            Title = title;
            Category = category;
            PublisherId = publisherId;
            YearPublished = yearPublished;
            Edition = edition;
            UnitPrice = unitPrice;
            AuthorIds = authorIds;
        }

        public string? Isbn { get; }

        public string? Title { get; }

        public BookCategory Category { get; }

        public int PublisherId { get; }

        public int YearPublished { get; }

        public int Edition { get; }

        public decimal UnitPrice { get; }

        public List<int>? AuthorIds { get; }
    }
}
