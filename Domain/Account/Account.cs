using Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Account
{
    public class Account : DomainAggregate
    {
        private Account()
        {
        }

        public Account(string? username, string? password) : this()
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new InvalidArgumentException(nameof(username));
            if (string.IsNullOrWhiteSpace(password))
                throw new InvalidArgumentException(nameof(password));

            Username = username;
            Password = password;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public virtual Profile? Profile { get; private set; }

        public void UpdateProfile(Profile profile)
        {
            Profile = profile;
        }
    }
}
