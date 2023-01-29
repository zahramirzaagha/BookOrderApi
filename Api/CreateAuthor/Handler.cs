using Domain.Book;
using MediatR;

namespace Api.CreateAuthor
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
            // Create a new author using the information passed through the command
            var author = new Author(request.FirstName, request.LastName, request.Email);

            // Add the new author to Entity Framework DB context
            _context.Authors.Add(author);

            // Dispatch possible domain events to give the listeners the opportunity to respond
            await author.DispatchDomainEventsAsync(_mediator, cancellationToken);

            // Save changes to the database.
            await _context.SaveChangesAsync(cancellationToken);

            // Return the id of the newly created author
            return author.Id;
        }
    }
}
