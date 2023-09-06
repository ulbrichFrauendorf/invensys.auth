using System.Security.Cryptography;

namespace invensys.auth.application.Common.Encryption;

public static class EncryptionService
{
    private static readonly KeyInfo KeyInfo = new KeyInfo("t3y55ODyB0uWdVeLREinqa6e1X3qOSVd7cCWvAlN3CE=", "+grqvacXAGs5x/WKiKLVpg==");
    //https://learn.microsoft.com/en-us/dotnet/standard/security/generating-keys-for-encryption-and-decryption
    //https://www.siakabaro.com/how-to-create-aes-encryption-256-bit-key-in-c/

    public static string Encrypt(string input)
    {
        var enc = EncryptStringToBytes_Aes(input, KeyInfo.Key, KeyInfo.Iv);
        return Convert.ToBase64String(enc);
    }

    public static string Decrypt(string cipherText)
    {
        var cipherBytes = Convert.FromBase64String(cipherText);
        return DecryptStringFromBytes_Aes(cipherBytes, KeyInfo.Key, KeyInfo.Iv);
    }

    private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
    {
        if (plainText is not { Length: > 0 })
        {
            throw new ArgumentNullException(nameof(plainText));
        }

        if (key is not { Length: > 0 })
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (iv is not { Length: > 0 })
        {
            throw new ArgumentNullException(nameof(iv));
        }

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }

        var encrypted = msEncrypt.ToArray();

        return encrypted;
    }

    private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
    {
        if (cipherText is not { Length: > 0 })
        {
            throw new ArgumentNullException(nameof(cipherText));
        }

        if (key is not { Length: > 0 })
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (iv is not { Length: > 0 })
        {
            throw new ArgumentNullException(nameof(iv));
        }

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var msDecrypt = new MemoryStream(cipherText);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        var plaintext = srDecrypt.ReadToEnd();

        return plaintext;
    }
}