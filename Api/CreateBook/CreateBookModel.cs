using Domain.Book;

namespace Api.CreateBook
{
    public class CreateBookModel
    {
        public string? Isbn { get; set; }

        public string? Title { get; set; }

        public BookCategory Category { get; set; }

        public int PublisherId { get; set; }

        public int YearPublished { get; set; }

        public int Edition { get; set; }

        public decimal UnitPrice { get; set; }

        public List<int>? AuthorIds { get; set; }

        public Command ToCommand()
        {
            return new Command(Isbn, Title, Category, PublisherId, YearPublished, Edition, UnitPrice, AuthorIds);
        }
    }
}
