using System.Text;
using lib.Lab.Models.Helpers;
using lib.Lab.Models.Interface;

namespace lib.Lab.Controllers.Lab9;

public class ShorthandBreakingSpace : IEncryptor
{
    private readonly string _sentenceSeparators = "\n";
    private readonly char _nonBreakingSpace = (char)160; // Десятичный код 160 для неразрывного пробела
    private string _key;

    public string Key
    {
        get => _key;
        set => _key = value;
    }

    public ShorthandBreakingSpace(string key)
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
            result.Append(line.Trim(' '));

            // Добавляем к строке до двух пробелов в соответствии с битами сообщения
            for (int i = 0; i < 2 && msgSymIndex < msgBits.Count; i++, msgSymIndex++)
            {
                // Добавляем пробел (обычный или неразрывный) в зависимости от бита сообщения
                result.Append((msgBits[msgSymIndex] == 0 ? ' ' : _nonBreakingSpace) + "\n");
            }

            // result.AppendLine(trimmedLine);
        }
        
        
        if (msgSymIndex != msgBits.Count)
            throw new Exception("Сообщение не влезло в контейнер");
        
        return result.ToString();
    }

    public string Decrypt(string msg)
    {
        List<int> result = new List<int>();
        StringBuilder resultStr = new StringBuilder();
        
        foreach (var line in msg.Split(_sentenceSeparators, StringSplitOptions.RemoveEmptyEntries))
        {
            var trimmedLine = line.TrimEnd(' ', _nonBreakingSpace);

            // Проверяем, были ли добавлены пробелы в конец строки
            for (int i = 0; i < 2; i++)
            {
                if (line.Length > trimmedLine.Length + i) // Проверяем, был ли удален пробел при обрезке
                {
                    char spaceChar = line[trimmedLine.Length + i]; // Получаем символ после обрезанной части
                    result.Add(spaceChar == _nonBreakingSpace ? 1 : 0);
                }
            }
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