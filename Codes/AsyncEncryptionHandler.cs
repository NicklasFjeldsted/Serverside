using System.Security.Cryptography;
using System.Text;

namespace Serverside.Codes
{
    public class AsyncEncryptionHandler
    {
        private string _privateKey;
        private string _publicKey;

        private readonly string _privateKeyPath = "./Keys/privateKey.xml";
        private readonly string _publicKeyPath = "./Keys/publicKey.xml";

        public AsyncEncryptionHandler()
        {
            if (File.Exists(_privateKeyPath) && File.Exists(_publicKeyPath))
            {
                // Keys already exist, load them
                _privateKey = File.ReadAllText(_privateKeyPath);
                _publicKey = File.ReadAllText(_publicKeyPath);
            }
            else
            {
                // Generate new keys and save them for future use
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048)) // Consider specifying the key size
                {
                    _privateKey = rsa.ToXmlString(true);
                    _publicKey = rsa.ToXmlString(false);
                    File.WriteAllText(_privateKeyPath, _privateKey);
                    File.WriteAllText(_publicKeyPath, _publicKey);
                }
            }
        }

        public string Encrypt(string textToEncrypt)
        {
            return AsyncEncrypter.Encrypt(textToEncrypt, _publicKey);
        }

        public string Decrypt(string textToDecrypt)
        {
            try
            {
                string base64EncodedData = textToDecrypt.Replace("-", "+").Replace("_", "/");

                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
                {
                    rsa.FromXmlString(_privateKey);

                    byte[] dataToDecryptAsByteArray = Convert.FromBase64String(base64EncodedData);
                    byte[] decryptedDataAsByteArray = rsa.Decrypt(dataToDecryptAsByteArray, true);
                    var decryptedDataAsString = Encoding.UTF8.GetString(decryptedDataAsByteArray);

                    return decryptedDataAsString;
                }
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging purposes
                Console.WriteLine($"Decryption failed: {ex.Message}");
                // Consider how you want to handle decryption failure. For example, you might return null or an error message.
                return null; // or "Decryption failed" or any other appropriate response
            }
        }
    }
}
