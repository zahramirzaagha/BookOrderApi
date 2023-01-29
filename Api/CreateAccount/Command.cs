using MediatR;

namespace Api.CreateAccount
{
    public class Command : IRequest<int>
    {
        public Command(string? username, string? password)
        {
            Username = username;
            Password = password;
        }

        public string? Username { get; }

        public string? Password { get; }
    }
}
