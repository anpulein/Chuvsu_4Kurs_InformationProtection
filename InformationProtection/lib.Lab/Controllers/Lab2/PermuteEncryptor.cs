using System.Text;
using lib.Lab.Models.Interface;

namespace lib.Lab.Controllers.Lab2;

public class PermuteEncryptor : IEncryptor
{
    private string _keyString; 
    private int[] _key;
    private int[] _reverseKey;

    public string Key
    {
        get => string.Join(",", _key);
        set
        {
            // Тот случай если ключ не изменялся
            if (_keyString == value)
                return;
            _keyString = value;
            _key = GetIntKey(value);
            _reverseKey = ReversedKey(_key);
        }
    }

    public PermuteEncryptor(string key) : base()
    {
        Key = key;
    }
    
    public string Encrypt(string msg)
    {
        // Key: 10,5,8,2,7,13,6,1,12,14,3,9,4,15,11
        // Value: Привет я Дима
        var output = $"Исходное слово: {msg}\n";
        var key = _key;
        
        var len = key.Length;
        var enc = new char[len];
        var result = new StringBuilder();
        
        msg = PrepareMsg(msg, len);
        
        for (int i = 0; i < msg.Length; i += len)
        {
            
            for (int j = 0; j < len; j++)
            {
                enc[j] = msg[i + (key[j] - 1)];
            }
        
            result.Append(enc);
        }
        
        output += $"Зашифрованное слово: {result}";
        return output;
    }

    public string Decrypt(string msg)
    {
        var output = $"Зашифрованное слово: {msg}\n";
        var key = _reverseKey;
        
        var len = key.Length;
        var enc = new char[len];
        var result = new StringBuilder();
        
        for (int i = 0; i < msg.Length; i += len)
        {
            
            for (int j = 0; j < len; j++)
            {
                enc[j] = msg[i + key[j]];
            }
        
            result.Append(enc);
        }
        
        output += $"Расшифрованное слово: {result}";
        return output;
    }

    /// <summary>
    /// Дополнение строки пробелами
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    private string PrepareMsg(string msg, int len)
    {
        var result = new StringBuilder(msg);
        var countSpace = len - (msg.Length % len);

        return result.Append(' ', countSpace).ToString();
    }

    /// <summary>
    /// Вычисление обратного ключа для расшифрования
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private int[] ReversedKey(int[] key)
    {
        var reverseKey = new int[key.Length];
        
        for (int i = 0; i < key.Length; i++)
        {
            reverseKey[i] = Array.IndexOf(key, i + 1);
        }
    
        return reverseKey;
    }

    /// <summary>
    /// Конвертить строку чисел ключа, в массив ключей типа Int
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private int[] GetIntKey(string key)
    {
        var strNumbers = key.Split(',');
        return Array.ConvertAll(strNumbers, int.Parse);
    }
    
    #region Test

    public string EncryptTest(string msg) => String.Empty;
    public string DecryptTest(string msg) => String.Empty;
    
    #endregion
}