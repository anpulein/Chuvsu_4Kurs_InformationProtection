using System.Text;
using System.Text.RegularExpressions;
using lib.Lab.Models.Helpers;
using lib.Lab.Models.Interface;

namespace lib.Lab.Controllers.Lab8;

public class IntervalBetweenSentencesEncryptor : IEncryptor
{
    private readonly string _sentenceSeparators = ".!?";
    private string _key;
    public string Key { 
        get => _key;
        set
        {
            _key = value;
            PrepareKey();
        }
    }

    public IntervalBetweenSentencesEncryptor(string key)
    {
        Key = key;
    }

    public string Encrypt(string msg)
    {
        if (string.IsNullOrEmpty(_key))
        {
            throw new InvalidOperationException("Для шифрования необходим контейнер");
        }
        
        var msgBits = DESHelpers.StringToBitList(msg);  // Переводим сообщение в список бит
        msgBits.Add(0);  // Последний ноль - маркер окончания сообщения
        var result = new StringBuilder();
        int msgSymIndex = 0;  // Указатель на бит, который надо вставить в контейнер

        for (int ind = 0; ind < Key.Length; ind++)
        {
            char sym = Key[ind];
            // Текущий символ надо добавлять всегда
            result.Append(sym);
            
            // Конец контейнера
            if (ind == Key.Length - 1 || msgSymIndex == msgBits.Count) continue;

            // Конец предложения и следующий символ пробел - место для внесения данных
            if (_sentenceSeparators.Contains(sym) && Key[ind + 1] == ' ')
            {
                result.Append(msgBits[msgSymIndex++] == 0 ? ' ' : string.Empty);
            }
        }
        
        if (msgSymIndex != msgBits.Count)
            throw new Exception("Сообщение не влезло в контейнер");
          
        return result.ToString();
    }

    public string Decrypt(string msg)
    {
        List<int> result = new List<int>();
        StringBuilder resultStr = new StringBuilder();
        for (int ind = 0; ind < msg.Length; ind++)
        {
            char sym = msg[ind];
            if (ind == msg.Length - 1) continue;

            if (_sentenceSeparators.Contains(sym))
            {
                int count = (msg[ind + 1] == ' ') ? 1 : 0;  // Следующий символ - пробел
                if (count > 0)
                {
                    // Рассматриваем, только если следующий символ пробел
                    count += ((ind + 2 <= msg.Length - 1) && msg[ind + 2] == ' ') ? 1 : 0;  // Через символ тоже пробел
                    result.Add(count % 2);
                }
            }
        }

        // Вспоминаем про маркер окончания сообщения.
        // Добираем до последнего нуля
        while (result.Last() != 0)
        {
            result.RemoveAt(result.Count - 1);
        }
        if (result.Last() == 0)
        {
            // И ноль тоже удаляем, т.к это маркер
            result.RemoveAt(result.Count - 1);
        }

        foreach (var block in DESHelpers.SplitTextToBlocks(result))
        {
            resultStr.Append(DESHelpers.GetBitListText(block));
        }

        return resultStr.ToString();
    }
    
    private void PrepareKey()
    {
        // foreach (var elem in _sentenceSeparators)
        // {
        //     string old = elem + "  ";
        //     string newStr = elem + " ";
        //     while (_key.Contains(old))
        //     {
        //         _key = _key.Replace(old, newStr);
        //     }
        // }
        foreach (var elem in _sentenceSeparators)
        {
            string pattern = Regex.Escape(elem.ToString()) + " {2,}";
            _key = Regex.Replace(_key, pattern, elem + " ");
        }
    }

    #region Test

    public string EncryptTest(string value) => Encrypt(value);
    public string DecryptTest(string value) => Decrypt(value);

    #endregion
}