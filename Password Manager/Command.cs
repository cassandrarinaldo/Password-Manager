using System.Text.Json;

namespace Projekt1
{
    public abstract class Command
    {
        protected string server;
        protected string client;
        protected string masterpwd;
        protected string secret_key;
        protected string IV;
        protected Dictionary<string, string> vault;
        protected EncryptDecrypt encryptDecrypt;
        //tar in en dictionary och gör om den till en sträng som sedan krypteras. Sedan skapas och skrivs både client och server fil. Secret-key blir här JSON-format
        protected void updateFiles(Dictionary<string, string> vault)
        {
            string stringDict = JsonSerializer.Serialize(vault);
            string encryptedDict = encryptDecrypt.encryptVault(stringDict);
            File.WriteAllText(server, IV + "\n");
            File.AppendAllText(server, encryptedDict);
            Dictionary<string, string> secretDict = new Dictionary<string, string>();
            secretDict.Add("Secret", secret_key);
            string secretKey = JsonSerializer.Serialize(secretDict);
            File.WriteAllText(client, secretKey);
        }
        //hämtar en dictionary från en fil (kan användas i båda)
        protected Dictionary<string, string> getVault(string server)
        {
            string[] encryptString = File.ReadLines(server).ToArray();
            string decryptString = encryptDecrypt.decryptVault(encryptString[1]);
            Dictionary<string, string> vault = JsonSerializer.Deserialize<Dictionary<string, string>>(decryptString);
            return vault;
        }
        public string getSecretKey()
        {
            string secret = File.ReadAllText(client);
            Dictionary<string, string> secretDict = JsonSerializer.Deserialize<Dictionary<string, string>>(secret);
            string secretKey = secretDict["Secret"];
            return secretKey;
        }
        protected string getIV()
        {
            string[] array = File.ReadLines(server).ToArray();
            return array[0];
        }
    }
}
