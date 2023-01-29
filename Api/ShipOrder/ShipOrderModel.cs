namespace Api.ShipOrder
{
    public class ShipOrderModel
    {
        public DateTimeOffset Date { get; set; }

        public Command ToCommand(int orderId)
        {
            return new Command(orderId, Date);
        }
    }
}
