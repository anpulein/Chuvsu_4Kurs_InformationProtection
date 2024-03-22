using System.Collections;
using System.Text;

namespace lib.Lab.Models.Helpers;

public static class DESHelpers
{
    public static string ExtendBin(this int dec, int fromBase = 2, int padLeft = 8) =>
        Convert.ToString(dec, fromBase).PadLeft(padLeft, '0');

    public static int[] BinToIntArray(this string str) => str.Select(ch => int.Parse(ch.ToString())).ToArray();
    
    public static int GetBin(this string str, int fromBase = 2) => Convert.ToInt32(str, fromBase);

    public static List<int> StringToBitList(this string str) => str.SelectMany(ch => ch.SymbolToBitList()).ToList();
    
    public static string GetBitListText(this List<int> bitList)
    {
        // Преобразование списка битов в строку
        var sb = new StringBuilder();
        for (int i = 0; i < bitList.Count; i += 8)
        {
            // Выбираем очередные 8 бит и преобразуем их в символ
            var chunk = bitList.Skip(i).Take(8).ToList();
            int dec = chunk.Aggregate((result, bit) => result * 2 + bit);
            sb.Append((char)dec);
        }
        

        return sb.ToString();
    }
    
    public static List<int> SymbolToBitList(this char ch, int padLeft = 8)
    {
        // Преобразование символа в 8-битное представление (байты)
        byte byteArray = BitConverter.GetBytes(ch)[0];
            
        var vi = Convert.ToString(byteArray, 2).PadLeft(padLeft, '0');
        return vi.BinToIntArray().ToList();
    }

    public static List<int> KeyPartCyclicShift(this List<int> keyPart, int index)
    {
        int val = Data.KeyShift[index];
        int len = keyPart.Count;

        var shiftedKeyPart = new List<int>();
        shiftedKeyPart.AddRange(keyPart.GetRange(val, len - val));
        shiftedKeyPart.AddRange(keyPart.GetRange(0, val));

        return shiftedKeyPart;
    }

    public static List<int> ListsXor(this List<int> list1, List<int> list2) =>
        list1.Select((item, index) => item ^ list2[index]).ToList();
    
    public static IEnumerable<string> SplitTextToBlocks(this string msg, int blockLen = 8)
    {
        // Дополним пробелами в конце только если нужно
        int need = msg.Length % blockLen;
        if (need > 0)
        {
            msg = msg.PadRight(msg.Length + (blockLen - need), ' ');
        }
        
        for (int ind = 0; ind < msg.Length; ind += blockLen)
        {
            yield return msg.Substring(ind, blockLen);
        }
    }
    
    public static IEnumerable<List<int>> SplitTextToBlocks(this List<int> list, int blockLen = 8)
    {
        List<int> block = new List<int>();
        foreach (var elem in list)
        {
            block.Add(elem);
            if (block.Count == blockLen)
            {
                yield return block.ToList();
                block.Clear();
            }
        }
    }

    public static List<int> ShiftLeft(this List<int> bitList, int shiftCount)
    {
        // Выполняем циклический сдвиг влево и создаем новый битовый список
        return new List<int>(bitList.Select((bit, index) =>
        {
            int newIndex = (index + shiftCount) % bitList.Count;
            return bitList[newIndex];
        }));
    }
    
    public static List<int> GammaGenerateKey(int length, int numLen = 8)
    {
        int A = 17;
        int C = 37;
        int T0 = 7;
        var key = new List<int>();

        for (int i = 0; i < length; i++)
        {
            char randomChar = (char)(T0 % 129); 
            key.AddRange(randomChar.SymbolToBitList(padLeft: numLen));

            T0 = (A * T0 + C) % 129;
        }

        return key;
    }
}