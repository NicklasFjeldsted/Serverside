using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;

namespace Serverside.Codes
{
    public class AsyncEncrypter
    {
        public static string Encrypt(string textToEncrypt, string publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.FromXmlString(publicKey);

                byte[] dataToEnctryptAsByteArray = Encoding.UTF8.GetBytes(textToEncrypt);
                byte[] encryptedDataAsByteArray = rsa.Encrypt(dataToEnctryptAsByteArray, true);
                var encrypedDataAsString = Convert.ToBase64String(encryptedDataAsByteArray);

                return encrypedDataAsString;
            }
        }
    }
}
