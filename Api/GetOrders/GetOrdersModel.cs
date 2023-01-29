using Domain.Order;

namespace Api.GetOrders
{
    public class GetOrdersModel
    {
        public int Id { get; set; }

        public string? OrderItems { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public decimal? Amount { get; set; }

        public DateTimeOffset? ScheduledDate { get; set; }

        public DateTimeOffset? ShippingDate { get; set; }

        public string? Status { get; set; }

        public static GetOrdersModel From(Order order)
        {
            return new GetOrdersModel
            {
                Id = order.Id,
                OrderItems = string.Join(", ", order.OrderItems.Select(x => x.Book.Isbn)),
                OrderDate = order.OrderDate,
                Amount = order.Amount,
                ScheduledDate = order.ScheduledDate,
                ShippingDate = order.ShippingDate,
                Status = Enum.GetName(order.Status)
            };
        }
    }
}