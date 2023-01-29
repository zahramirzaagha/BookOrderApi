using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.ConfirmOrder
{
    [ApiController]
    [Route("api/orders/{orderId}")]
    public class Controller : ControllerBase
    {
        private readonly ILogger<Controller> _logger;
        private readonly IMediator _mediator;

        public Controller(ILogger<Controller> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// This method will capture PUT requests to this resource URL: https://localhost:7166/api/orders/{orderId}/confirm
        /// which will confirm the <see cref="Domain.Order.Order"/>
        /// </summary>
        /// <param name="orderId">
        /// The id of a previously created <see cref="Domain.Order.Order"/> to be confirmed
        /// </param>
        /// <returns></returns>
        [HttpPut("confirm")]
        public async Task<ActionResult> Put([FromRoute] int orderId)
        {
            // This will send a command to the mediator to confirm an order
            await _mediator.Send(new Command(orderId));

            // This will set the HTTP response status code to 200 (Ok)
            return Ok();
        }
    }
}