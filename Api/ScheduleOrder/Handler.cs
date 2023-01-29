using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.ScheduleOrder
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
            // Fetch the order from the database
            var order = await _context.Orders.FindAsync(request.OrderId);

            // Throw if order not found
            if (order == null)
                throw new OrderNotFoundException(request.OrderId);

            // Schedule the order shippment
            order.Schedule(request.Date);

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
