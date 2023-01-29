using Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Account
{
    public class Profile
    {
        private Profile()
        {
        }

        public Profile(string? name, string? address, string? city, string? postalCode, string? phoneNumber, string? email) : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidArgumentException(nameof(name));
            if (string.IsNullOrWhiteSpace(address))
                throw new InvalidArgumentException(nameof(address));
            if (string.IsNullOrWhiteSpace(city))
                throw new InvalidArgumentException(nameof(city));
            if (string.IsNullOrWhiteSpace(postalCode))
                throw new InvalidArgumentException(nameof(postalCode));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new InvalidArgumentException(nameof(phoneNumber));
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidArgumentException(nameof(email));
            if (!new EmailAddressAttribute().IsValid(email))
                throw new InvalidArgumentException(nameof(email));
            
            Name = name;
            Address = address;
            City = city;
            PostalCode = postalCode;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Address { get; private set; }

        public string City { get; private set; }

        public string PostalCode { get; private set; }

        public string PhoneNumber { get; private set; }

        public string Email { get; private set; }
    }
}
