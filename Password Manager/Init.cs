using System.Security.Cryptography;

namespace Projekt1
{
    public class Init : Command
    {
        public Init(string client, string server, string pwd)
        {
            base.server = server;
            base.client = client;
            masterpwd = pwd;
            secret_key = createSecretKey();
            IV = createIV();
            encryptDecrypt = new EncryptDecrypt(secret_key, masterpwd, IV);
            vault = new Dictionary<string, string>();
            updateFiles(vault);
        }
        private string createSecretKey()
        {
            RandomNumberGenerator.Create();
            byte[] array = RandomNumberGenerator.GetBytes(16);
            return Convert.ToBase64String(array);
        }
        private string createIV()
        {
            Aes test = Aes.Create();
            test.GenerateIV();
            byte[] array = test.IV;
            return Convert.ToBase64String(array);
        }
    }
}