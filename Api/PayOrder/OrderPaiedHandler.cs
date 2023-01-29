using Domain.Exceptions;
using Domain.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.PayOrder
{
    public class OrderPaiedHandler : INotificationHandler<OrderPayed>
    {
        private readonly BookOrderDbContext _context;
        private readonly IMediator _mediator;

        public OrderPaiedHandler(BookOrderDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task Handle(OrderPayed notification, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Book)
                .SingleAsync(x => x.Id == notification.OrderId);
            if (order == null)
                throw new OrderNotFoundException(notification.OrderId);

            foreach (var orderItem in order.OrderItems)
            {
                var book = await _context.Books.FindAsync(orderItem.Book.Id);
                if (book == null)
                    throw new BookNotFoundException(orderItem.Book.Id);

                book.DecreaseInventory(orderItem.Quantity);
                _context.Entry(book).State = EntityState.Modified;
                await book.DispatchDomainEventsAsync(_mediator, cancellationToken);
            }
        }
    }
}
