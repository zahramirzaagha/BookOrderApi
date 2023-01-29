namespace Api.CreatePublisher
{
    public class CreatePublisherModel
    {
        public string? Name { get; set; }
        
        public Command ToCommand()
        {
            return new Command(Name);
        }
    }
}
