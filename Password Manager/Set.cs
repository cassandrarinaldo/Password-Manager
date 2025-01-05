namespace Projekt1
{
    public class Set : Command
    {
        public Set(string client, string server, string pwd)
        {
            base.server = server;
            base.client = client;
            masterpwd = pwd;
            secret_key = getSecretKey();
            IV = getIV();
        }
        public void SetDomain(string domain, string pwd)
        {
            //if (CheckLength(masterpwd))
            //{
                try
                {
                    encryptDecrypt = new EncryptDecrypt(secret_key, masterpwd, IV);
                    string[] encryptString = File.ReadLines(server).ToArray();
                    string decryptString = encryptDecrypt.decryptVault(encryptString[1]);
                    Dictionary<string, string> vault = getVault(server);
                    vault.Add(domain, pwd);
                    updateFiles(vault);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Decryption failed");
                }
            //}
            //else
            //{
            //    Console.WriteLine("Wrong masterpassword");
            //}
        }
    }
}
