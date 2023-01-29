using Domain.Book;

namespace Api.GetBooks
{
    public class GetBooksModel
    {
        public int Id { get; set; }

        public string? Isbn { get; set; }

        public string? Title { get; set; }

        public string? Category { get; set; }

        public string? Publisher { get; set; }

        public int YearPublished { get; set; }

        public int Edition { get; set; }

        public decimal UnitPrice { get; set; }

        public string? Authors { get; set; }

        public static GetBooksModel From(Book book)
        {
            return new GetBooksModel
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                Category = Enum.GetName(book.Category),
                Publisher = book.Publisher.Name,
                YearPublished = book.YearPublished,
                Edition = book.Edition,
                UnitPrice = book.UnitPrice,
                Authors = string.Join(", ", book.Authors.Select(x => x.FirstName + " " + x.LastName))
            };
        }
    }
}