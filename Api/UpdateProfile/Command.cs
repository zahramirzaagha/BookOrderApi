using MediatR;

namespace Api.UpdateProfile
{
    public class Command : IRequest
    {
        public Command(int accountId, string? name, string? address, string? city, string? postalCode, string? phoneNumber, string? email)
        {
            AccountId = accountId;
            Name = name;
            Address = address;
            City = city;
            PostalCode = postalCode;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public int AccountId { get; }
        
        public string? Name { get; }
        
        public string? Address { get; }
        
        public string? City { get; }
        
        public string? PostalCode { get; }
        
        public string? PhoneNumber { get; }
        
        public string? Email { get; }
    }
}
