namespace Domain.Exceptions
{
    public class AuthorNotFoundException : Exception
    {
        public AuthorNotFoundException(int authorId) : base($"Author not found. AuthorId: {authorId}")
        {
        }
    }
}
