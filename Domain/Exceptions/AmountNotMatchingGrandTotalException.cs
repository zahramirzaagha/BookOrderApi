namespace Domain.Exceptions
{
    public class AmountNotMatchingGrandTotalException : Exception
    {
        public AmountNotMatchingGrandTotalException(int orderId, decimal grandTotal, decimal amount)
            : base($"Amount does not match grand total. OrderId: {orderId}. GrandTotal: {grandTotal}. Amount: {amount}")
        {
        }
    }
}