using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.GetBooks
{
    public class Handler : IRequestHandler<Query, IEnumerable<GetBooksModel>>
    {
        private readonly BookOrderDbContext _context;

        public Handler(BookOrderDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<GetBooksModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var books = _context.Books
                .Include(x => x.Publisher)
                .Include(x => x.Authors)
                .Select(x => GetBooksModel.From(x)).AsEnumerable();
            
            return Task.FromResult(books);
        }
    }
}
