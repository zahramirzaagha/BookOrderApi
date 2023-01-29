using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.PayOrder
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
        /// This method will capture PUT requests to this resource URL: https://localhost:7166/api/orders/{orderId}/pay
        /// which will update the <see cref="Domain.Order.Order"/> with payment information
        /// </summary>
        /// <param name="orderId">
        /// The id of a previously created <see cref="Domain.Order.Order"/> to be updated with payment information
        /// </param>
        /// <param name="model">
        /// The model is of type <see cref="PayOrderModel"/>
        /// which captures information required pay for the <see cref="Domain.Order.Order"/>
        /// </param>
        /// <returns></returns>
        [HttpPut("pay")]
        public async Task<ActionResult> Put([FromRoute] int orderId, [FromBody] PayOrderModel model)
        {
            // This will send a command to the mediator to pay for an order
            await _mediator.Send(model.ToCommand(orderId));

            // This will set the HTTP response status code to 200 (Ok)
            return Ok();
        }
    }
}