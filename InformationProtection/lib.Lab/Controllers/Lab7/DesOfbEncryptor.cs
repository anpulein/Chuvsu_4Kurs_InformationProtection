using System.Text;
using lib.Lab.Models.Helpers;
using lib.Lab.Models.Interface;

namespace lib.Lab.Controllers.Lab4;

public class DesOfbEncryptor : IEncryptor
{
    public string Key { get; set; }
    private readonly DES _desEncryptor;
    private readonly List<int> _previousCipherBlock;
    private readonly int _kBits;

    public DesOfbEncryptor(string key)
    {
        Key = key;
        _desEncryptor = new DES(key);
        _kBits = 8;
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
    
    private string Encode(string message)
    {
        var result = new StringBuilder();
        
        var previousCipherBlock = _previousCipherBlock;

        foreach (var block in message.SplitTextToBlocks(blockLen: _kBits))
        {
            var feedbackBlock = _desEncryptor.EncodeBlock(previousCipherBlock);
            var processedBlock = block.StringToBitList();
            
            previousCipherBlock = previousCipherBlock.ShiftLeft(block.Length);
            
            var encryptedBlock = processedBlock.ListsXor(feedbackBlock);
            
            // var encodedBlock = _desEncryptor.EncodeBlock(processedBlock);
            var encodeStr = encryptedBlock.GetBitListText();
            result.Append(encodeStr);
            
        }

        return result.ToString();
    }
    
    private string Decode(string message)
    {
        var result = new StringBuilder();

        var previousCipherBlock = _previousCipherBlock;
        
        foreach (var block in message.SplitTextToBlocks(blockLen: _kBits))
        {
            
            var feedbackBlock = _desEncryptor.EncodeBlock(previousCipherBlock);
            var processedBlock = block.StringToBitList();
            
            previousCipherBlock = previousCipherBlock.ShiftLeft(block.Length);
            
            var decryptedBlock = processedBlock.ListsXor(feedbackBlock);
            
            // var encodedBlock = _desEncryptor.EncodeBlock(processedBlock);
            var encodeStr = decryptedBlock.GetBitListText();
            result.Append(encodeStr);
        }

        return result.ToString();
    }

    #region Test

    public string EncryptTest(string msg) => Encode(msg);
    public string DecryptTest(string msg) => Decode(msg);
    
    #endregion
}