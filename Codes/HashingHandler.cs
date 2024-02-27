using System.Security.Cryptography;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace Serverside.Codes
{
    public class HashingHandler
    {
        private byte[] m_inputBytes = null;

        public HashingHandler(string textToHash)
        {
            m_inputBytes = Encoding.ASCII.GetBytes(textToHash);
        }

        public string MD5Hashing()
        {
            if (m_inputBytes == null)
            {
                throw new NullReferenceException("Input bytes cannot be null.");
            }

            MD5 md5 = MD5.Create();
            byte[] hashedValue = md5.ComputeHash(m_inputBytes);
            return Convert.ToBase64String(hashedValue);
        }

        public string SHA1Hashing()
        {
            if (m_inputBytes == null)
            {
                throw new NullReferenceException("Input bytes cannot be null.");
            }

            SHA1 sha1 = SHA1.Create();
            byte[] hashedValue = sha1.ComputeHash(m_inputBytes);
            return Convert.ToBase64String(hashedValue);
        }

        public string HMACHashing()
        {
            if(m_inputBytes == null)
            {
                throw new NullReferenceException("Input bytes cannot be null.");
            }

            HMACSHA256 hmac = new HMACSHA256();
            byte[] myKey = Encoding.ASCII.GetBytes("SkalDuMunkes");
            hmac.Key = myKey;
            byte[] hashedValue = hmac.ComputeHash(m_inputBytes);
            return Convert.ToBase64String(hashedValue);
        }

        public string PBKDF2Hashing(string salt, string hashAlgorithmName)
        {
            if(m_inputBytes == null)
            {
                throw new NullReferenceException("Input bytes cannot be null.");
            }

            byte[] saltAsByteArray = Encoding.ASCII.GetBytes(salt);
            var hashAlgorithm = new HashAlgorithmName(hashAlgorithmName);

            byte[] hashedValue = Rfc2898DeriveBytes.Pbkdf2(m_inputBytes, saltAsByteArray, 10, hashAlgorithm, 32);
            return Convert.ToBase64String(hashedValue);
        }

        public static string BCryptHashing(string textToHash)
        {
            return BC.HashPassword(textToHash, 10, true);
        }

        public static bool BCryptVerify(string textToHash, string hashValue)
        {
            return BC.Verify(textToHash, hashValue, true);
        }   
    }
}
