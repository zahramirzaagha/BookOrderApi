using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.GetOrders
{
    public class Handler : IRequestHandler<Query, IEnumerable<GetOrdersModel>>
    {
        private readonly BookOrderDbContext _context;

        public Handler(BookOrderDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<GetOrdersModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var books = _context.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Book)
                .Select(x => GetOrdersModel.From(x)).AsEnumerable();
            
            return Task.FromResult(books);
        }
    }
}
