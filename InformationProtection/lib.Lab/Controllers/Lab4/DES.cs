using System.Collections;
using System.Text;
using InformationProtection.lib.Lab.Models.Interface;
using lib.Lab.Models.Helpers;

namespace lib.Lab.Controllers.Lab4;

public class DES : IDES
{
    private readonly int _lenKey = 8;
    private readonly List<List<int>> _keys;
    private readonly List<List<int>> _reverseKeys;
    private readonly SBox _sBox;

    private readonly Permute _initBlock;
    private readonly Permute _reverseBlock;
    private readonly Permute _block;
    private readonly Permute _keyPermute1;
    private readonly Permute _keyPermute2;

    public DES(string key)
    {
        if (key.Length != _lenKey)
            throw new Exception("Длина ключа должгна быть равна 8");

        _initBlock = new Permute(Data.InitBlock);
        _reverseBlock = new Permute(Data.RevBlock);
        _block = new Permute(Data.Block);
        _keyPermute1 = new Permute(Data.KeyPermute1);
        _keyPermute2 = new Permute(Data.KeyPermute2);

        _sBox = new SBox();
        
        _keys = GetKeys(key);
        _reverseKeys = _keys.ToArray().Reverse().ToList();
    }

    private List<List<int>> GetKeys(string keyStr)
    {
        var bitKey = keyStr.SelectMany(ch => ch.SymbolToBitList()).ToList();
        var processedBitKey = _keyPermute1.Get(bitKey);

        var keyLeftPart = processedBitKey.GetRange(0, 28);
        var keyRightPart = processedBitKey.GetRange(28, 28);

        var keys = Enumerable.Range(0, 16)
            .Select(i =>
            {
                keyLeftPart = keyLeftPart.KeyPartCyclicShift(i);
                keyRightPart = keyRightPart.KeyPartCyclicShift(i);
                return _keyPermute2.Get(keyLeftPart.Concat(keyRightPart).ToList());
            })
            .ToList();

        return keys;
    }

    public List<int> EncodeBlock(List<int> block)
    {
        // Применение начальной перестановки
        var processedBlock = _initBlock.Get(block);
        
        // Разделение блока на левую и правую часть
        var leftHalf = processedBlock.GetRange(0, 32);
        var rightHalf = processedBlock.GetRange(32, 32);

        // 16 раундов шифровки
        for (int i = 0; i < 16; i++)
        {
            var roundKey = _keys[i];
            var kRoundFunc = RoundFunc(rightHalf, roundKey);
            leftHalf = leftHalf.ListsXor(kRoundFunc);
            
            if (i != 15)
            {
                (leftHalf, rightHalf) = (rightHalf, leftHalf);
            }

        }

        processedBlock = leftHalf.Concat(rightHalf).ToList();
        
        // Примерение обратной перестановки
        return _reverseBlock.Get(processedBlock);
    }
    
    public List<int> DecodeBlock(List<int> block)
    {
        // Применение начальной перестановки
        var processedBlock = _initBlock.Get(block);
        
        // Разделение блока на левую и правую часть
        var leftHalf = processedBlock.GetRange(0, 32);
        var rightHalf = processedBlock.GetRange(32, 32);

        // 16 раундов шифровки
        for (int i = 0; i < 16; i++)
        {
            var roundKey = _reverseKeys[i];
            var kRoundFunc = RoundFunc(rightHalf, roundKey);
            leftHalf = leftHalf.ListsXor(kRoundFunc);
            
            if (i != 15)
            {
                (leftHalf, rightHalf) = (rightHalf, leftHalf);
            }
        }

        processedBlock = leftHalf.Concat(rightHalf).ToList();

        // Примерение обратной перестановки
        return _reverseBlock.Get(processedBlock);
    }

    private List<int> RoundFunc(List<int> rightPartBlock, List<int> key)
    {
        // Применение перестановки
        var block = Permute.Get(Data.KeyExtend, rightPartBlock);

        // Примерение XOR к блоку
        block = block.ListsXor(key);

        // Разделение блока на части 
        var parts = Enumerable.Range(0, block.Count / 6)
            .Select(index => block.Skip(index * 6).Take(6).ToList())
            .ToList();
        

        // Применение S-Box преобразования
        var transformedParts = parts.Select((part, index) => _sBox.GetSsTransform(index, part)).ToList();
        
        // Объдинение результатов
        return _block.Get(transformedParts.SelectMany(part => part).ToList());
    }
}