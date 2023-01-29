namespace Api.CreateAuthor
{
    public class CreateAuthorModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public Command ToCommand()
        {
            return new Command(FirstName, LastName, Email);
        }
    }
}
