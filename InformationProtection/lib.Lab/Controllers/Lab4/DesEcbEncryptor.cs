using System.Text;
using lib.Lab.Models.Helpers;
using lib.Lab.Models.Interface;

namespace lib.Lab.Controllers.Lab4;

public class DesEcbEncryptor : IEncryptor
{
    public string Key { get; set; }
    private readonly DES _desEncryptor;

    public DesEcbEncryptor(string key)
    {
        Key = key;
        _desEncryptor = new DES(key);
    }

    public string Encrypt(string msg)
    {
        var output = $"Исходное слово: {msg}\n";

        var result = Encode(msg);

        output += $"Зашифрованное слово: {result}";
        return output;
    }

    public string Decrypt(string msg)
    {
        var output = $"Зашифрованное слово: {msg}\n";

        var result = Decode(msg);

        output += $"Расшифрованное слово: {result}";
        return output;
    }
    
    private string Encode(string message)
    {
        var result = new StringBuilder();

        foreach (var block in message.SplitTextToBlocks())
        {
            var newBlock = block.StringToBitList();
            var encodedBlock = _desEncryptor.EncodeBlock(newBlock);
            var encodeStr = encodedBlock.GetBitListText();
            result.Append(encodeStr);
        }

        return result.ToString();
    }
    
    private string Decode(string message)
    {
        var result = new StringBuilder();

        foreach (var block in message.SplitTextToBlocks())
        {
            var newBlock = block.StringToBitList();
            var decodedBlock = _desEncryptor.DecodeBlock(newBlock);
            var decodeStr = decodedBlock.GetBitListText();
            result.Append(decodeStr);
        }

        return result.ToString();
    }

    #region Test

    public string EncryptTest(string msg) => Encode(msg);
    public string DecryptTest(string msg) => Decode(msg);
    
    #endregion
}