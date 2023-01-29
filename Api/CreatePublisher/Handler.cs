using Domain.Book;
using MediatR;

namespace Api.CreatePublisher
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
            // Create a new publisher using the information passed through the command
            var publisher = new Publisher(request.Name);

            // Add the new publisher to Entity Framework DB context
            _context.Publishers.Add(publisher);

            // Dispatch possible domain events to give the listeners the opportunity to respond
            await publisher.DispatchDomainEventsAsync(_mediator, cancellationToken);

            // Save changes to the database.
            await _context.SaveChangesAsync(cancellationToken);

            // Return the id of the newly created author
            return publisher.Id;
        }
    }
}
