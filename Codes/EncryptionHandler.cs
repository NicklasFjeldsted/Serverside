using Microsoft.AspNetCore.DataProtection;

namespace Serverside.Codes
{
    public class EncryptionHandler
    {
        private readonly IDataProtector _protector;

        public EncryptionHandler(IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.CreateProtector("AlexanderSkalMunkes");
        }

        public string Encrypt(string textToEncrypt) =>
            _protector.Protect(textToEncrypt);

        public string Decrypt(string textToDecrypt) =>
            _protector.Unprotect(textToDecrypt);
    }
}
