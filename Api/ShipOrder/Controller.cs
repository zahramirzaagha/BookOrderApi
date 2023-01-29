using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.ShipOrder
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
        /// This method will capture PUT requests to this resource URL: https://localhost:7166/api/orders/{orderId}/ship
        /// which will update the <see cref="Domain.Order.Order"/> information with the exact delivery date
        /// </summary>
        /// <param name="orderId">
        /// The id of a previously created <see cref="Domain.Order.Order"/> to be shipped
        /// </param>
        /// <param name="model">
        /// The model is of type <see cref="ShipOrderModel"/>
        /// which captures information required to ship the <see cref="Domain.Order.Order"/>
        /// </param>
        /// <returns></returns>
        [HttpPut("ship")]
        public async Task<ActionResult> Put([FromRoute] int orderId, [FromBody] ShipOrderModel model)
        {
            // This will send a command to the mediator to update an order with the exact shippment date
            await _mediator.Send(model.ToCommand(orderId));

            // This will set the HTTP response status code to 200 (Ok)
            return Ok();
        }
    }
}