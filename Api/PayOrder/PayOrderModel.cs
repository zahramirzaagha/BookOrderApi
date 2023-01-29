namespace Api.PayOrder
{
    public class PayOrderModel
    {
        public decimal Amount { get; set; }

        public Command ToCommand(int orderId)
        {
            return new Command(orderId, Amount);
        }
    }
}
