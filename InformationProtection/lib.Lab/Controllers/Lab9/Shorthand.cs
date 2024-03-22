using System.Text;
using lib.Lab.Models.Helpers;
using lib.Lab.Models.Interface;

namespace lib.Lab.Controllers.Lab9;

public class Shorthand : IEncryptor
{
    private readonly string _sentenceSeparators = "\n";
    private readonly char _nonBreakingSpace = (char)160; // Десятичный код 160 для неразрывного пробела
    private string _key;

    public string Key
    {
        get => _key;
        set => _key = value;
    }

    public Shorthand(string key)
    {
        _key = key;
    }
    

    public string Encrypt(string msg)
    {

        if (string.IsNullOrEmpty(_key))
            throw new InvalidOperationException("Для шифрования необходим контейнер");
        
        List<int> msgBits = DESHelpers.StringToBitList(msg);
        StringBuilder result = new StringBuilder();

        int msgSymIndex = 0;

        foreach (var line in _key.Split(_sentenceSeparators))
        {
            line.Trim(' ');

            // Сообщение находится полностью в контейнере
            if (msgSymIndex == msgBits.Count)
            {
                result.Append(line + '\n');
                continue;
            }
            
            result.Append(line + (msgBits[msgSymIndex++] == 1 ? " \n" : "  \n"));

        }
        
        
        if (msgSymIndex != msgBits.Count)
            throw new Exception("Сообщение не влезло в контейнер");
        
        return result.ToString();
    }

    public string Decrypt(string msg)
    {
        List<int> result = new List<int>();
        StringBuilder resultStr = new StringBuilder();

        foreach (var line in msg.Split(_sentenceSeparators))
        {
            // Строка не заканчивается на пробел, а значит больше нет строк с пробелами
            if (line[line.Length - 1] != ' ') break;
        
            int count = (line[line.Length - 1] == ' ') ? 1 : 0;
            count += (line.Length >= 2 && line[line.Length - 2] == ' ') ? 1 : 0;
        
            // 0 Кодируется 2 пробелами, 1 - 1 пробелом
            result.Add(count % 2);
        }
        
        
        foreach (var block in DESHelpers.SplitTextToBlocks(result))
        {
            resultStr.Append(DESHelpers.GetBitListText(block));
        }

        return resultStr.ToString();
    }

    #region Test

    public string EncryptTest(string value) => Encrypt(value);

    public string DecryptTest(string value) => Decrypt(value);

    #endregion
}
