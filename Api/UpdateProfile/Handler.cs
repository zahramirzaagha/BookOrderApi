using Domain.Account;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.UpdateProfile
{
    public class Handler : IRequestHandler<Command>
    {
        private readonly BookOrderDbContext _context;
        private readonly IMediator _mediator;

        public Handler(BookOrderDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            // Fetch the account from the database
            var account = await _context.Accounts.FindAsync(request.AccountId);

            // Throw if account not found
            if (account == null)
                throw new AccountNotFoundException(request.AccountId);

            // Create a new profile using the information passed through the command
            var profile = new Profile(request.Name, request.Address, request.City, request.PostalCode, request.PhoneNumber, request.Email);
            
            // Update the profile of the account
            account.UpdateProfile(profile);

            // Mark the enrty in Entity Framework context as modified to force an update
            _context.Entry(account).State = EntityState.Modified;

            // Dispatch possible domain events to give the listeners the opportunity to respond
            await account.DispatchDomainEventsAsync(_mediator, cancellationToken);

            // Save changes to the database.
            await _context.SaveChangesAsync(cancellationToken);

            // Return nothing!
            return Unit.Value;
        }
    }
}
