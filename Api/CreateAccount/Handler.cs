using Domain.Account;
using MediatR;

namespace Api.CreateAccount
{
    public class Handler : IRequestHandler<Command, int>
    {
        private readonly BookOrderDbContext _context;
        private readonly IMediator _mediator;

        public Handler(BookOrderDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            // Create a new account using the information passed through the command
            var account = new Account(request.Username, request.Password);

            // Add the new account to Entity Framework DB context
            _context.Accounts.Add(account);

            // Dispatch possible domain events to give the listeners the opportunity to respond
            await account.DispatchDomainEventsAsync(_mediator, cancellationToken);

            // Save changes to the database.
            await _context.SaveChangesAsync(cancellationToken);

            // Return the id of the newly created account
            return account.Id;
        }
    }
}
