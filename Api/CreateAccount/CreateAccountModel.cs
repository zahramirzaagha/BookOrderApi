namespace Api.CreateAccount
{
    public class CreateAccountModel
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        public Command ToCommand()
        {
            return new Command(Username, Password);
        }
    }
}
