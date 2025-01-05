using System.Security.Cryptography;
using System.Text;

namespace Projekt1
{
    public class EncryptDecrypt
    {
        private string secretKey;
        private string masterpwd;
        private byte[] vaultKey;
        private byte[] IV;
        private Aes aes = Aes.Create();

        public EncryptDecrypt(string key, string pwd, string iv)
        {
            secretKey = key;
            masterpwd = pwd;
            vaultKey = Rfc2898DeriveBytes.Pbkdf2(Convert.FromBase64String(secretKey), Encoding.UTF8.GetBytes(masterpwd), 1000, HashAlgorithmName.SHA384, 16);
            IV = Convert.FromBase64String(iv);
        }
        public string encryptVault(string text)
        {
            ICryptoTransform encryptor = aes.CreateEncryptor(vaultKey, IV);
            byte[] encrypted;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return Convert.ToBase64String(encrypted);
        }
        public string decryptVault(string text)
        {
            byte[] textToDecrypt = Convert.FromBase64String(text);

            ICryptoTransform decryptor = aes.CreateDecryptor(vaultKey, IV);
            string decryptedText = null;
            using (MemoryStream msDecrypt = new MemoryStream(textToDecrypt))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        decryptedText = srDecrypt.ReadToEnd();
                    }
                }
            }
            return decryptedText;
        }
    }
}