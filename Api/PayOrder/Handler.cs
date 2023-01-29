using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.PayOrder
{
    public class Handler : IRequestHandler<Command>
    {
        private readonly BookOrderDbContext _context;
        private readonly IMediator _mediator;

        public Handler(BookOrderDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            // Fetch the order from the database (including also the order items and in each item the associated book used to calculate the grand total)
            var order = await _context.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Book)
                .SingleAsync(x => x.Id == request.OrderId);

            // Throw if order not found
            if (order == null)
                throw new OrderNotFoundException(request.OrderId);

            // Pay for the order
            order.Pay(request.Amount);

            // Mark the enrty in Entity Framework context as modified to force an update
            _context.Entry(order).State = EntityState.Modified;

            // Dispatch possible domain events to give the listeners the opportunity to respond
            await order.DispatchDomainEventsAsync(_mediator, cancellationToken);

            // Save changes to the database.
            _context.SaveChanges();

            // Return nothing!
            return Unit.Value;
        }
    }
}
