using Domain.Exceptions;
using Domain.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.CreateOrder
{
    public class Handler : IRequestHandler<Command, int>
    {
        private readonly BookOrderDbContext _context;
        private readonly IMediator _mediator;

        public Handler(BookOrderDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            // Fetch the account from the database (including the profile for the account used for validating the account)
            // No order is allowed to be created for an account without a profile
            var account = await _context.Accounts
                .Include(x => x.Profile)
                .SingleAsync(x => x.Id == request.AccountId, cancellationToken);

            // Throw if account not found
            if (account == null)
                throw new AccountNotFoundException(request.AccountId);

            // Create a new order using the information passed through the command
            var order = new Order(account);

            // Add each order item to the newly created order
            foreach (var orderItem in request.OrderItems)
            {
                // Fetch the book form the database
                var book = await _context.Books.FindAsync(orderItem.BookId, cancellationToken);

                // Throw if book not found
                if (book == null)
                    throw new BookNotFoundException(orderItem.BookId);

                // Add order item to the book
                order.AddOrderItem(book, orderItem.Quantity);
            }

            // Add the new order to Entity Framework DB context
            _context.Orders.Add(order);

            // Dispatch possible domain events to give the listeners the opportunity to respond
            await order.DispatchDomainEventsAsync(_mediator, cancellationToken);

            // Save changes to the database.
            await _context.SaveChangesAsync(cancellationToken);

            // Return the id of the newly created order
            return order.Id;
        }
    }
}
