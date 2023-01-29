using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.CreateOrder
{
    [ApiController]
    [Route("api/orders")]
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
        /// This method will capture POST requests to this resource URL: https://localhost:7166/api/orders
        /// </summary>
        /// <param name="model">
        /// The model is of type <see cref="CreateOrderModel"/>
        /// The model captures information required to create a new <see cref="Domain.Order.Order"/>
        /// </param>
        /// <returns>
        /// Created order id in Json format:
        /// {
        ///     "orderId": 1
        /// }
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateOrderModel model)//this model is bound to the payload(body)
                                                                                    //of the request which is json document
        {
            // This will send a command to the mediator to create a new order
            // and will return the id of the newly created order
            int orderId = await _mediator.Send(model.ToCommand());

            // This is wrapping the order id in a Json document and setting the HTTP response status code to 200 (Ok)
            return base.Ok(new { OrderId = orderId });
        }
    }
}