using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.AddInventory
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
            // Fetch the book from the database
            var book = await _context.Books.FindAsync(request.BookId);

            // Throw if book not found
            if (book == null)
                throw new BookNotFoundException(request.BookId);

            // Increase the inventory of the book by value
            book.IncreaseInventory(request.Inventory);

            // Mark the enrty in Entity Framework context as modified to force an update
            _context.Entry(book).State = EntityState.Modified;

            // Dispatch possible domain events to give the listeners the opportunity to respond
            await book.DispatchDomainEventsAsync(_mediator, cancellationToken);

            // Save changes to the database.
            _context.SaveChanges();

            // Return nothing!
            return Unit.Value;
        }
    }
}
