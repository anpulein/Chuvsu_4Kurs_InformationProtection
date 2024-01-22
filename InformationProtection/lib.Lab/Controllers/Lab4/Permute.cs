using lib.Lab.Models.Interface.DES;

namespace lib.Lab.Controllers.Lab4;

public class Permute : IPermute
{
    private readonly List<int> _key;

    public Permute(int[] key)
    {
        _key = key.ToList();
    }

    public List<int> Get(List<int> block) => _key.Select(index => block[index - 1]).ToList();
    public static List<int> Get(List<int> key, List<int> block) => key.Select(index => block[index - 1]).ToList();
    
}