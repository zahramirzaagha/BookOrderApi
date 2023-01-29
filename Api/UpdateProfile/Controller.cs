using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.UpdateProfile
{
    [ApiController]
    [Route("api/accounts/{accountId}")]
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
        /// This method will capture PUT requests to this resource URL: https://localhost:7166/api/accounts/{accountId}/updateprofile
        /// which will create a new <see cref="Domain.Account.Profile"/> for an existing <see cref="Domain.Account.Account"/>
        /// </summary>
        /// <param name="accountId">
        /// The id of a previously created <see cref="Domain.Account.Account"/> for which the
        /// <see cref="Domain.Account.Profile"/> is created
        /// </param>
        /// <param name="model">
        /// The model is of type <see cref="UpdateProfileModel"/>
        /// The model captures information required to create a new <see cref="Domain.Account.Profile"/>
        /// </param>
        /// <returns></returns>
        [HttpPut("updateprofile")]
        public async Task<ActionResult<int>> Post([FromRoute] int accountId, [FromBody] UpdateProfileModel model)
        {
            // This will send a command to the mediator to create a profile for an existing account
            await _mediator.Send(model.ToCommand(accountId));

            // This will set the HTTP response status code to 200 (Ok)
            return Ok();
        }
    }
}