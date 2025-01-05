using System.Text.Json;

namespace Projekt1
{
    public class Create : Command
    {
        public Create(string client, string server, string pwd, string secret_key)
        {
            this.client = client;
            this.server = server;
            masterpwd = pwd;
            this.secret_key = secret_key;
            IV = getIV();
            CreateClient();
        }
        public void CreateClient()
        {
            if (secret_key.Length % 4 == 0)
            {
                try
                {
                    encryptDecrypt = new EncryptDecrypt(secret_key, masterpwd, IV);
                    string[] encryptString = File.ReadLines(server).ToArray();
                    string decryptString = encryptDecrypt.decryptVault(encryptString[1]);
                    Dictionary<string, string> secretDict = new Dictionary<string, string>();
                    secretDict.Add("Secret", secret_key);
                    string secretKey = JsonSerializer.Serialize(secretDict);
                    File.WriteAllText(client, secretKey);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Decryption failed");
                }
            }
            else
            {
                Console.WriteLine("Wrong secret key");
            }

        }
    }
}
