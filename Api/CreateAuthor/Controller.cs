using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.CreateAuthor
{
    [ApiController]
    [Route("api/authors")]
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
        /// This method will capture POST requests to this resource URL: https://localhost:7166/api/authors
        /// which will be used to create a new <see cref="Domain.Book.Author"/>. The newly created <see cref="Domain.Book.Author"/>
        /// can then be used to create a <see cref="Domain.Book.Book"/>
        /// </summary>
        /// <param name="model">
        /// The model is of type <see cref="CreateAuthorModel"/>
        /// The model captures information required to create a new <see cref="Domain.Book.Author"/>
        /// </param>
        /// <returns>
        /// Created <see cref="Domain.Book.Author"/>.Id in Json format:
        /// {
        ///     "authorId": 1
        /// }
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateAuthorModel model)
        {
            // This will send a command to the mediator to create a new author
            // and will return the id of the newly created author
            int authorId = await _mediator.Send(model.ToCommand());

            // This is wrapping the author id in a Json document and setting the HTTP response status code to 200 (Ok)
            return base.Ok(new { AuthorId = authorId });
        }
    }
}