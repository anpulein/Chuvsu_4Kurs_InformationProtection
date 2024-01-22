using lib.Lab.Models.Interface.DES;

namespace lib.Lab.Models.Extensions;

public static class PermuteExtensions
{
    public static int[] Get(int[] key, int[] block) =>
        key.Select(index => block[index - 1]).ToArray();
}