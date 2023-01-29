namespace Domain.Exceptions
{
    internal class AccountProfileMissing : Exception
    {
        public AccountProfileMissing(int accountId) : base($"Account profile missing. AccountId: {accountId}")
        {
        }
    }
}