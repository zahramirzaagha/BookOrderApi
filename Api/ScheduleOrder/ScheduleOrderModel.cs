namespace Api.ScheduleOrder
{
    public class ScheduleOrderModel
    {
        public DateTimeOffset Date { get; set; }

        public Command ToCommand(int orderId)
        {
            return new Command(orderId, Date);
        }
    }
}
