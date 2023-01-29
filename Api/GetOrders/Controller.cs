using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.GetOrders
{
    [ApiController]
    [Route("api/orders/{accountId}")]
    public class Controller : ControllerBase
    {
        private readonly ILogger<Controller> _logger;
        private readonly IMediator _mediator;

        public Controller(ILogger<Controller> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrdersModel>>> Get(int accountId)
        {
            var orders = await _mediator.Send(new Query(accountId));
            return base.Ok(orders);
        }
    }
}