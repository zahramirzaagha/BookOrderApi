using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Order
{
    public class OrderItem
    {
        private OrderItem()
        {
        }

        public OrderItem(Order order, Book.Book book, int quantity)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            Order = order;
            Book = book;
            Quantity = quantity;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        public virtual Order Order { get; private set; }

        public virtual Book.Book Book { get; private set; }

        public int Quantity { get; private set; }

        public decimal Total
        {
            get
            {
                return Quantity * Book.UnitPrice;
            }
        }
    }
}