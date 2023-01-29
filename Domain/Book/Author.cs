using Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Book
{
    public class Author : DomainAggregate
    {
        private Author()
        {
        }

        public Author(string? firstName, string? lastName, string? email) : this()
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new InvalidArgumentException(nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new InvalidArgumentException(nameof(lastName));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));
            if (!new EmailAddressAttribute().IsValid(email))
                throw new InvalidArgumentException(nameof(email));

            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public List<Book> Books { get; private set; }
    }
}
