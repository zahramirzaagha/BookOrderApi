using Domain.Book;
using Domain.Exceptions;
using MediatR;

namespace Api.CreateBook
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
            // Fetch the publisher from the database
            var publisher = await _context.Publishers.FindAsync(request.PublisherId, cancellationToken);
            
            // Throw if publisher not found
            if (publisher == null)
                throw new PublisherNotFoundException(request.PublisherId);

            // Create a new book using the information passed through the command
            var book = new Book(request.Isbn, request.Title, request.Category, publisher, request.YearPublished, request.Edition, request.UnitPrice);
            
            // Add each author to the newly created book
            foreach (var authorId in request.AuthorIds)
            {
                // Fetch the author form the database
                var author = await _context.Authors.FindAsync(authorId, cancellationToken);
                
                // Throw if author not found
                if (author == null)
                    throw new AuthorNotFoundException(authorId);

                // Add author to the book
                book.AddAuthor(author);
            }

            // Add the new book to Entity Framework DB context
            _context.Books.Add(book);

            // Dispatch possible domain events to give the listeners the opportunity to respond
            await book.DispatchDomainEventsAsync(_mediator, cancellationToken);

            // Save changes to the database.
            await _context.SaveChangesAsync(cancellationToken);

            // Return the id of the newly created book
            return book.Id;
        }
    }
}
