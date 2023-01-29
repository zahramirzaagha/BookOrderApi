using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.CreateAccount
{
    [ApiController]
    [Route("api/accounts")]
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
        /// This method will capture POST requests to this resource URL: https://localhost:7166/api/accounts
        /// which will create a new <see cref="Domain.Account.Account"/>. The newly created <see cref="Domain.Account.Account"/>
        /// can then be used to create <see cref="Domain.Order.Order"/> for
        /// </summary>
        /// <param name="model">
        /// The model is of type <see cref="CreateAccountModel"/>
        /// The model captures information required to create a new <see cref="Domain.Account.Account"/>
        /// </param>
        /// <returns>
        /// Created <see cref="Domain.Account.Account"/>.Id in Json format:
        /// {
        ///     "accountId": 1
        /// }
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateAccountModel model)
        {
            // This will send a command to the mediator to create a new account
            // and will return the id of the newly created account
            int accountId = await _mediator.Send(model.ToCommand());

            // This is wrapping the account id in a Json document and setting the HTTP response status code to 200 (Ok)
            return base.Ok(new { AccountId = accountId });
        }
    }
}