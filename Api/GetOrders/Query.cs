using MediatR;

namespace Api.GetOrders
{
    public class Query : IRequest<IEnumerable<GetOrdersModel>>
    {
        public Query(int accountId)
        {
            AccountId = accountId;
        }

        public int AccountId { get; }
    }
}
