using System.Text;

namespace lib.Lab.Controllers.Lab1;

public class ReplaceEncryptor
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

    public ReplaceEncryptor() {}
    
    public ReplaceEncryptor(string key) : base()
    {
        Key = key;
        _reverseKey = ReversedKey(key);
    }
    
    public string Encrypt(string msg)
    {
        var aCode = 'А';
        var result = new StringBuilder();

        for (int i = 0; i < msg.Length; i++)
        {
            result.Append(Key[msg[i] - aCode]);
        }
        
        return result.ToString();
    }

    public string Decrypt(string value)
    {
        return String.Empty;
    }

    private string ReversedKey(string key)
    {
        
        return String.Empty;
    }
}