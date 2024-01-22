using System.Text;
using lib.Lab.Models.Interface;

namespace lib.Lab.Controllers.Lab3;

public class GammingEncryptor : IEncryptor 
{
    public string Key { get; set; }

    public GammingEncryptor(int length)
    {
        Key = GenerateKey(length);
    }

    public string Encrypt(string msg)
    {
        //var output = $"Исходное слово: {msg}\n";
        var output = String.Empty;

        var result = Gamming(msg);

        output += $"Зашифрованное слово: {result}";
        return output;
    }

    public string Decrypt(string msg)
    {
        var output = $"Зашифрованное слово: {msg}\n";
        
        var result = Gamming(msg);
        
        output += $"Расшифрованное слово: {result}";
        return output;
    }

    private string Gamming(string msg)
    {
        StringBuilder encryptedText = new StringBuilder();

        for (int i = 0; i < msg.Length; i++)
        {
            char plainChar = msg[i];
            char keyChar = Key[i % Key.Length]; 

            char encryptedChar = (char)(plainChar ^ keyChar); // XOR шифрование
            encryptedText.Append(encryptedChar);
        }

        return encryptedText.ToString();
    }

    private string GenerateKey(int length)
    {
        int A = 17;
        int C = 37;
        int T0 = 7;
        var key = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            char randomChar = (char)(T0 % 129); 
            key.Append(randomChar);

            T0 = (A * T0 + C) % 129;
        }

        return key.ToString();
    }
    
    #region Test

    public string EncryptTest(string msg) => String.Empty;
    public string DecryptTest(string msg) => String.Empty;
    
    #endregion
}