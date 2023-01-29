using MediatR;

namespace Api.CreatePublisher
{
    public class Command : IRequest<int>
    {
        public Command(string? name)
        {
            Name = name;
        }

        public string? Name { get; }
    }
}
