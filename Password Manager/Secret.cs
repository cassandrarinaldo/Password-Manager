namespace Projekt1
{
    public class Secret : Command
    {
        public Secret(string client)
        {
            base.client = client;
            secret_key = getSecretKey();
        }
        public string returnSecret()
        {
            return secret_key;
        }
    }
}
