using lib.Lab.Models.Enum;

namespace lib.Lab.Models.Interface;

public interface ILabFactory
{
    IEncryptor Create(Labs typeLab, string key, int length);
}