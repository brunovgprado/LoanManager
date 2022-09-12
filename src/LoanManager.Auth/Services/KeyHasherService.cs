using System;
using System.Security.Cryptography;
using System.Text;

namespace LoanManager.Auth.Services
{
    public class KeyHasherService
    {
        private HashAlgorithm _algorithm;

        public KeyHasherService(HashAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public string EncriptKey(string password)
        {
            var encodedValue = Encoding.UTF8.GetBytes(password);
            var encryptedPassword = _algorithm.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var character in encryptedPassword)
            {
                sb.Append(character.ToString("X2"));
            }

            return sb.ToString();
        }

        public bool VerifyKey(string key, string persistedKey)
        {
            if (string.IsNullOrEmpty(persistedKey))
                throw new ArgumentNullException();

            var encryptedKey = _algorithm.ComputeHash(Encoding.UTF8.GetBytes(key));

            var sb = new StringBuilder();
            foreach (var character in encryptedKey)
            {
                sb.Append(character.ToString("X2"));
            }

            return sb.ToString() == persistedKey;
        }
    }
}
