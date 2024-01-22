using System.Text;
using lib.Lab.Controllers.Lab4;
using lib.Lab.Models.Helpers;
using lib.Lab.Models.Interface;

namespace lib.Lab.Controllers.Lab5;

public class DesCbcEncryptor : IEncryptor
{
    public string Key { get; set; }
    private readonly DES _desEncryptor;
    private readonly List<int> _previousCipherBlock;

    public DesCbcEncryptor(string key)
    {
        Key = key;
        _desEncryptor = new DES(key);
        _previousCipherBlock = DESHelpers.GammaGenerateKey(8);
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
    
    private string Encode(string messages)
    {
        var result = new StringBuilder();

        var previousCipherBlock = _previousCipherBlock;

        foreach (var block in messages.SplitTextToBlocks())
        {
            var processedBlock = block.StringToBitList();
            
            processedBlock = processedBlock.ListsXor(previousCipherBlock);
            
            var encodedBlock = _desEncryptor.EncodeBlock(processedBlock);
            var encodeStr = encodedBlock.GetBitListText();
            result.Append(encodeStr);
            
            // сохраням текущий шифртекст для следующей итерации
            previousCipherBlock = encodedBlock;
        }

        return result.ToString();
    }
    
    private string Decode(string messages)
    {
        var result = new StringBuilder();
        
        var previousCipherBlock = _previousCipherBlock;

        foreach (var block in messages.SplitTextToBlocks())
        {
            var processedBlock = block.StringToBitList();
            var decodedBlock = _desEncryptor.DecodeBlock(processedBlock);
            
            decodedBlock = decodedBlock.ListsXor(previousCipherBlock);
            
            var decodeStr = decodedBlock.GetBitListText();
            result.Append(decodeStr);

            // сохраням текущий шифртекст для следующей итерации
            previousCipherBlock = processedBlock;
        }

        return result.ToString();
    }
    
    #region Test

    public string EncryptTest(string msg) => Encode(msg);
    public string DecryptTest(string msg) => Decode(msg);
    
    #endregion
}