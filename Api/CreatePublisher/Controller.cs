using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.CreatePublisher
{
    [ApiController]
    [Route("api/publishers")]
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
        /// This method will capture POST requests to this resource URL: https://localhost:7166/api/publishers
        /// which will be used to create a new <see cref="Domain.Book.Publisher"/>. The newly created <see cref="Domain.Book.Publisher"/>
        /// can then be used to create a <see cref="Domain.Book.Book"/>
        /// </summary>
        /// <param name="model">
        /// The model is of type <see cref="CreatePublisherModel"/>
        /// The model captures information required to create a new <see cref="Domain.Book.Publisher"/>
        /// </param>
        /// <returns>
        /// Created <see cref="Domain.Book.Publisher"/>.Id in Json format:
        /// {
        ///     "publisherId": 1
        /// }
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreatePublisherModel model)
        {
            // This will send a command to the mediator to create a new publisher
            // and will return the id of the newly created publisher
            int publisherId = await _mediator.Send(model.ToCommand());

            // This is wrapping the publisher id in a Json document and setting the HTTP response status code to 200 (Ok)
            return base.Ok(new { PublisherId = publisherId });
        }
    }
}