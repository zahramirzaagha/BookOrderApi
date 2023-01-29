namespace Domain.Exceptions
{
    public class PublisherNotFoundException : Exception
    {
        public PublisherNotFoundException(int publisherId) : base($"Publisher not found. PublisherId: {publisherId}")
        {
        }
    }
}
