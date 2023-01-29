using MediatR;

namespace Api.UpdateProfile
{
    public class UpdateProfileModel
    {
        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? PostalCode { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        internal IRequest ToCommand(int accountId)
        {
            return new Command(accountId, Name, Address, City, PostalCode, PhoneNumber, Email);
        }
    }
}
