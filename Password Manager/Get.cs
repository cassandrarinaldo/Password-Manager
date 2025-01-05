namespace Projekt1
{
    public class Get : Command
    {
        public Get(string client, string server, string pwd)
        {
            base.server = server;
            base.client = client;
            masterpwd = pwd;
            secret_key = getSecretKey();
            IV = getIV();
        }
        public void GetDomain()
        {
            try
            {
                encryptDecrypt = new EncryptDecrypt(secret_key, masterpwd, IV);
                string[] encryptString = File.ReadLines(server).ToArray();
                string decryptString = encryptDecrypt.decryptVault(encryptString[1]);
                Dictionary<string, string> vault = getVault(server);
                foreach (var pair in vault)
                {
                    Console.WriteLine(pair.Key);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Decryption failed");
            }
        }
        public void GetPwd(string domain)
        {
            try
            {
                encryptDecrypt = new EncryptDecrypt(secret_key, masterpwd, IV);
                string[] encryptString = File.ReadLines(server).ToArray();
                string decryptString = encryptDecrypt.decryptVault(encryptString[1]);
                Dictionary<string, string> vault = getVault(server);
                Console.WriteLine(vault[domain]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Decryption failed");
            }
        }
    }
}
