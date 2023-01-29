using MediatR;

namespace Api.CreateAuthor
{
    public class Command : IRequest<int>
    {
        public Command(string? firstName, string? lastName, string? email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string? FirstName { get; }

        public string? LastName { get; }

        public string? Email { get; }
    }
}
