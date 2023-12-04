using System.IO;
using System.Security.Cryptography;

namespace GreenThumb.Managers
{
    internal static class KeyManager
    {
        public static string GetEncryptionKey()
        {
            // The path for the "Key.txt" file
            string path = Path.Combine(Environment.CurrentDirectory, "key.txt");

            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                string key = GenerateEncryptionKey();
                File.WriteAllText(path, key);

                return key;
            }
        }

        public static string GenerateEncryptionKey()
        {
            var rng = new RNGCryptoServiceProvider();
            var byteArray = new byte[16];
            rng.GetBytes(byteArray);
            return Convert.ToBase64String(byteArray);
        }
    }
}
