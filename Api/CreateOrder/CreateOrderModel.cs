namespace Api.CreateOrder
{
    public class CreateOrderModel
    {
        public int AccountId { get; set; }

        public IEnumerable<OrderItemModel>? OrderItems { get; set; }

        public Command ToCommand()
        {
            return new Command(AccountId, OrderItems?.Select(x => new OrderItem(x.BookId, x.Quantity)));
        }
    }

    public class OrderItemModel
    {
        public int BookId { get; set; }

        public int Quantity { get; set; }
    }
}
