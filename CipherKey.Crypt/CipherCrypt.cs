using System.Security.Cryptography;

namespace CipherKey.Crypt;

public class CipherCrypt
{
    private const int KeySize = 256;
    private const int BlockSize = 128;

    public static void EncryptFile(string inputFile, string outputFile, string password)
    {
        using (var inputStream = new FileStream(inputFile, FileMode.Open))
        using (var outputStream = new FileStream(outputFile, FileMode.Create))
        {
            var key = GenerateKey(password);
            var iv = GenerateIV(password);

            using (var aes = Aes.Create())
            {
                aes.KeySize = KeySize;
                aes.BlockSize = BlockSize;
                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor())
                using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                {
                    inputStream.CopyTo(cryptoStream);
                }
            }
        }
    }

    public static void DecryptFile(string inputFile, string outputFile, string password)
    {
        using (var inputStream = new FileStream(inputFile, FileMode.Open))
        using (var outputStream = new FileStream(outputFile, FileMode.Create))
        {
            var key = GenerateKey(password);
            var iv = GenerateIV(password);

            using (var aes = Aes.Create())
            {
                aes.KeySize = KeySize;
                aes.BlockSize = BlockSize;
                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                using (var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(outputStream);
                }
            }
        }
    }

    private static byte[] GenerateKey(string password)
    {
        var deriveBytes = new Rfc2898DeriveBytes(password, 8);
        return deriveBytes.GetBytes(KeySize / 8);
    }

    private static byte[] GenerateIV(string password)
    {
        var deriveBytes = new Rfc2898DeriveBytes(password, 16);
        return deriveBytes.GetBytes(BlockSize / 8);
    }
}