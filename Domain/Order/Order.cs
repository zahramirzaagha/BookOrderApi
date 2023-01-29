using Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Order
{
    public class Order : DomainAggregate
    {
        public const decimal TaxRate = 0.15M;

        private Order()
        {
            OrderDate = DateTimeOffset.UtcNow;
            OrderItems = new List<OrderItem>();
            Status = OrderStatus.Pending;
        }

        public Order(Account.Account? account) : this()
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            if (account.Profile == null)
                throw new AccountProfileMissing(account.Id);

            Account = account;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        public virtual Account.Account? Account { get; private set; }

        public List<OrderItem> OrderItems { get; private set; }

        public DateTimeOffset OrderDate { get; private set; }
        
        public decimal? Amount { get; private set; }

        public DateTimeOffset? ScheduledDate { get; private set; }

        public DateTimeOffset? ShippingDate { get; private set; }

        public OrderStatus Status { get; private set; }

        public void AddOrderItem(Book.Book book, int quantity)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            var orderItem = new OrderItem(this, book, quantity);
            OrderItems.Add(orderItem);
        }

        public void Confirm()
        {
            if (!CanConfirm)
                throw new InvalidOperationException($"Can't confirm order. Id: {Id}. Status: {Status}");

            Status = OrderStatus.Confirmed;
            AddDomainEvent(new OrderConfirmed(Id));
        }

        public bool CanConfirm
        {
            get
            {
                if (!CanChange)
                    return false;
                if (Status != OrderStatus.Pending)
                    return false;
                
                return true;
            }
        }

        public void Pay(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            if (amount != GetGrandTotal())
                throw new AmountNotMatchingGrandTotalException(Id, GetGrandTotal(), amount);
            if (!CanPay)
                throw new InvalidOperationException($"Can't pay order. Id: {Id}. Status: {Status}");

            Amount = amount;
            Status = OrderStatus.Paied;
            AddDomainEvent(new OrderPayed(Id));
        }

        public bool CanPay
        {
            get
            {
                if (!CanChange)
                    return false;
                if (Status != OrderStatus.Confirmed)
                    return false;
                
                return true;
            }
        }

        public void Schedule(DateTimeOffset date)
        {
            if (date <= DateTimeOffset.UtcNow)
                throw new ArgumentOutOfRangeException(nameof(date));
            if (!CanSchedule)
                throw new InvalidOperationException($"Can't schedule order. Id: {Id}. Status: {Status}");

            ScheduledDate = date;
            Status = OrderStatus.Scheduled;
            AddDomainEvent(new OrderScheduled(Id));
        }

        public bool CanSchedule
        {
            get
            {
                if (!CanChange)
                    return false;
                if (Status != OrderStatus.Paied)
                    return false;
                
                return true;
            }
        }

        public void Ship(DateTimeOffset date)
        {
            if (date <= DateTimeOffset.UtcNow)
                throw new ArgumentOutOfRangeException(nameof(date));
            if (!CanShip)
                throw new InvalidOperationException($"Can't ship order. Id: {Id}. Status: {Status}");

            ShippingDate = date;
            Status = OrderStatus.Shipped;
            AddDomainEvent(new OrderShipped(Id));
        }

        public bool CanShip
        {
            get
            {
                if (!CanChange)
                    return false;
                if (Status != OrderStatus.Scheduled)
                    return false;
                
                return true;
            }
        }

        public void Cancel()
        {
            if (!CanCancel)
                throw new InvalidOperationException($"Can't cancel order. Id: {Id}. Status: {Status}");

            var statusWhenCanceling = Status;
            Status = OrderStatus.Canceled;
            AddDomainEvent(new OrderCanceled(Id, statusWhenCanceling));
        }

        public bool CanCancel
        {
            get
            {
                if (!CanChange)
                    return false;

                return true;
            }
        }

        public bool CanChange
        {
            get
            {
                if (Status == OrderStatus.Canceled)
                    return false;
                if (Status == OrderStatus.Shipped)
                    return false;
                
                return true;
            }
        }

        public decimal GetOrderTotal()
        {
            decimal total = 0;
            foreach (var orderItem in OrderItems)
                total = total + orderItem.Total;
            
            return total;
        }

        public decimal GetTaxTotal()
        {
            return GetOrderTotal() * TaxRate;
        }

        public decimal GetGrandTotal()
        {
            decimal grandTotal = GetTaxTotal() + GetOrderTotal();
            return decimal.Round(grandTotal, 2, MidpointRounding.AwayFromZero);
        }
    }
}
