using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.GetBooks
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBooksModel>>> Get()
        {
            var books = await _mediator.Send(new Query());
            return base.Ok(books);
        }
    }
}