using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.CreateBook
{
    [ApiController]
    [Route("api/books")]
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
        /// This method will capture POST requests to this resource URL: https://localhost:7166/api/books
        /// </summary>
        /// <param name="model">
        /// The model is of type <see cref="CreateBookModel"/>
        /// The model captures information required to create a new <see cref="Domain.Book.Book"/>
        /// </param>
        /// <returns>
        /// Created <see cref="Domain.Book.Book"/>.Id in Json format:
        /// {
        ///     "bookId": 1
        /// }
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateBookModel model)
        {
            // This will send a command to the mediator to create a new book
            // and will return the id of the newly created book
            int bookId = await _mediator.Send(model.ToCommand());

            // This is wrapping the book id in a Json document and setting the HTTP response status code to 200 (Ok)
            return base.Ok(new { BookId = bookId });
        }
    }
}