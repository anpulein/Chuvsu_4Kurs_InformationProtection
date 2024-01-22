using lib.Lab.Controllers.Lab1;
using lib.Lab.Models.Enum;
using lib.Lab.Models.Interface;

namespace lib.Lab.Providers;

public class Worker
{
    private readonly ILabFactory _labFactory;

    public Worker()
    {
        _labFactory = new LabFactory();
    }
    
    public IEncryptor CreateLabInstance(Labs typeLab, string key, int length = 0)
    {
        return _labFactory.Create(typeLab, key, length);
    }

    public string GetResult(string typeCoder, IEncryptor encryptor, string inputValue)
    {
        return typeCoder == "encoder" ? encryptor.Encrypt(inputValue) : encryptor.Decrypt(inputValue);
    }
}