namespace Projekt1
{
    public class Delete : Command
    {
        public Delete(string client, string server, string pwd)
        {
            base.server = server;
            base.client = client;
            masterpwd = pwd;
            secret_key = getSecretKey();
            IV = getIV();
        }
        public void DeleteDomain(string domain)
        {
            try
            {
                encryptDecrypt = new EncryptDecrypt(secret_key, masterpwd, IV);
                string[] encryptString = File.ReadLines(server).ToArray();
                string decryptString = encryptDecrypt.decryptVault(encryptString[1]);
                Dictionary<string, string> vault = getVault(server);
                if (vault.Remove(domain)) {
                    updateFiles(vault);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Decryption failed"); //felmeddelanden mer generella?
            }
        }
    }
}
