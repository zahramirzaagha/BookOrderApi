namespace Domain.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException(int accountId) : base($"Account not found. AccountId: {accountId}")
        {
        }
    }
}
