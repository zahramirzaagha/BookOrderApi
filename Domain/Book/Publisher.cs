using Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Book
{
    public class Publisher : DomainAggregate
    {
        private Publisher()
        {
        }

        public Publisher(string? name) : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidArgumentException(nameof(name));

            Name = name;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
