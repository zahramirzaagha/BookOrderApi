using MediatR;

namespace Api.GetBooks
{
    public class Query : IRequest<IEnumerable<GetBooksModel>>
    {
    }
}
