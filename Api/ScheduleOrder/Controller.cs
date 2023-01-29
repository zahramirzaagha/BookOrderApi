using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.ScheduleOrder
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
        /// This method will capture PUT requests to this resource URL: https://localhost:7166/api/orders/{orderId}/schedule
        /// which will schedule the <see cref="Domain.Order.Order"/> shippment for a specific date
        /// </summary>
        /// <param name="orderId">
        /// The id of a previously created <see cref="Domain.Order.Order"/> to be scheduled
        /// </param>
        /// <param name="model">
        /// The model is of type <see cref="ScheduleOrderModel"/>
        /// which captures information required to schedule the <see cref="Domain.Order.Order"/>
        /// </param>
        /// <returns></returns>
        [HttpPut("schedule")]
        public async Task<ActionResult> Put([FromRoute] int orderId, [FromBody] ScheduleOrderModel model)
        {
            // This will send a command to the mediator to schedule shippment of an order
            await _mediator.Send(model.ToCommand(orderId));

            // This will set the HTTP response status code to 200 (Ok)
            return Ok();
        }
    }
}