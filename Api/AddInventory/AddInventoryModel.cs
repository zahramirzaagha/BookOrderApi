namespace Api.AddInventory
{
    public class AddInventoryModel
    {
        public int Inventory { get; set; }

        public Command ToCommand(int bookId)
        {
            return new Command(bookId, Inventory);
        }
    }
}
