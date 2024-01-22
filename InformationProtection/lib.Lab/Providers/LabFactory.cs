using lib.Lab.Controllers.Lab1;
using lib.Lab.Controllers.Lab2;
using lib.Lab.Controllers.Lab3;
using lib.Lab.Controllers.Lab4;
using lib.Lab.Controllers.Lab5;
using lib.Lab.Models.Enum;
using lib.Lab.Models.Interface;

namespace lib.Lab.Providers;

public class LabFactory : ILabFactory
{
    public IEncryptor Create(Labs typeLab, string key, int length) => typeLab switch
    {
        Labs.Lab1 => new ReplaceEncryptor(key),
        Labs.Lab2 => new PermuteEncryptor(key),
        Labs.Lab3 => new GammingEncryptor(length),
        Labs.Lab4 => new DesEcbEncryptor(key),
        Labs.Lab5 => new DesCbcEncryptor(key),
        Labs.Lab6 => new DesCfbEncryptor(key),
        Labs.Lab7 => new DesOfbEncryptor(key),
        // Labs.Lab8 => new ReplaceEncryptor(key),
        // Labs.Lab9 => new ReplaceEncryptor(key),
        _ => throw new InvalidOperationException("Лабораторная работа недоступна")
    };
}

