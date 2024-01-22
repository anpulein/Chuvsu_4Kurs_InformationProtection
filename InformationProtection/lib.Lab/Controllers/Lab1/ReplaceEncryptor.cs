using System.Text;
using lib.Lab.Models.Interface;

namespace lib.Lab.Controllers.Lab1;

public class ReplaceEncryptor : IEncryptor
{
    private string _reverseKey;
    private string _key;

    public string Key
    {
        get => _key;
        set
        {
            _key = value;
            _reverseKey = ReversedKey(value);
        }
    }
    
    public ReplaceEncryptor(string key) : base()
    {
        _key = key;
        _reverseKey = ReversedKey(key);
    }
    
    public string Encrypt(string msg)
    {
        var output = $"Исходное слово: {msg}\n";
        
        var aCode = 'А';
        var result = new StringBuilder();

        for (int i = 0; i < msg.Length; i++)
        {
            var index = msg[i] - aCode;
            result.Append(Key[index]);
        }

        output += $"Зашифрованное слово: {result}";
        return output;
    }

    public string Decrypt(string msg)
    {
        var output = $"Зашифрованное слово: {msg}\n";
        
        var aCode = 'А';
        var result = new StringBuilder();

        for (int i = 0; i < msg.Length; i++)
        {
            var index = msg[i] - aCode;
            result.Append(_reverseKey[index]);
        }
        
        output += $"Расшифрованное слово: {result}";
        return output;
    }

    private char ToPreparedE(int index, char value, string key)
    {
        if (value == 'Ё') return key[6];
        else if (value < 'А' || value > 'Я') return value;
        else if (index <= 5) return key[index];
        else return key[index + 1];
    }
    
    private string ReversedKey(string key)
    {
        // ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ
        var alphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        var code = 'А';
        
        // Создаем StringBuilder для обратного ключа
        var reversedKey = new char[key.Length];

        for (int i = 0; i < key.Length; i++)
        {
            int index = key[i] - code;
            reversedKey[index] = alphabet[i];
        }
        
        return new string(reversedKey);
    }
    
    #region Test

    public string EncryptTest(string msg) => String.Empty;
    public string DecryptTest(string msg) => String.Empty;
    
    #endregion
}