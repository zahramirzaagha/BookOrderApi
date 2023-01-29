using Domain.Exceptions;
using Domain.Order;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Book
{
    public class Book : DomainAggregate
    {
        private Book()
        {
            Authors = new List<Author>();
        }

        public Book(string? isbn, string? title, BookCategory category, Publisher publisher, int yearPublished, int edition, decimal unitPrice)
            : this()
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new InvalidArgumentException(nameof(isbn));
            if (string.IsNullOrWhiteSpace(title))
                throw new InvalidArgumentException(nameof(title));
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));
            if (yearPublished > DateTime.Today.Year)
                throw new InvalidArgumentException(nameof(yearPublished));
            if (edition < 1)
                throw new InvalidArgumentException(nameof(edition));
            if (unitPrice <= 0)
                throw new InvalidArgumentException(nameof(unitPrice));

            Isbn = isbn;
            Title = title;
            Category = category;
            Publisher = publisher;
            YearPublished = yearPublished;
            Edition = edition;
            UnitPrice = unitPrice;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        public string Isbn { get; private set; }

        public string Title { get; private set; }

        public BookCategory Category { get; private set; }

        public virtual Publisher Publisher { get; private set; }

        public int YearPublished { get; private set; }

        public int Edition { get; private set; }

        public decimal UnitPrice { get; private set; }

        public List<Author> Authors { get; private set; }

        public int Inventory { get; private set; }

        public void AddAuthor(Author author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            Authors.Add(author);
        }

        public void DecreaseInventory(int quantity)
        {
            Inventory -= quantity;
            AddDomainEvent(new InventoryDecreased(Id));
        }

        public void IncreaseInventory(int quantity)
        {
            Inventory += quantity;
            AddDomainEvent(new InventoryIncreased(Id));
        }

        public void ReturnInventory(int quantity, OrderStatus orderStatusWhenCanceling)
        {
            if (orderStatusWhenCanceling < OrderStatus.Paied)
                return;

            IncreaseInventory(quantity);
        }
    }
}
