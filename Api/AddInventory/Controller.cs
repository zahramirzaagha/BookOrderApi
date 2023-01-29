using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.AddInventory
{
    [ApiController]
    [Route("api/books/{bookId}")]
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
        /// This method will capture PUT requests to this resource URL: https://localhost:7166/api/books/{bookId}/addinventory
        /// which adds a specific quantity to a <see cref="Domain.Book.Book"/> inventory
        /// </summary>
        /// <param name="bookId">
        /// The id of a previously created <see cref="Domain.Book.Book"/> for which we wish to increase the inventory
        /// </param>
        /// <param name="model">
        /// The model is of type <see cref="AddInventoryModel"/>
        /// The model captures information required to add a quantity to the inventory of a <see cref="Domain.Book.Book"/>
        /// </param>
        /// <returns></returns>
        [HttpPut("addinventory")]
        public async Task<ActionResult> Put([FromRoute] int bookId, [FromBody] AddInventoryModel model)
        {
            // This will send a command to the mediator to add a specific quantity to a book's inventory
            await _mediator.Send(model.ToCommand(bookId));

            // This will set the HTTP response status code to 200 (Ok)
            return Ok();
        }
    }
}