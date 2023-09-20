using lib.Lab.Models.Interface;

namespace lib.Lab.Provider;

public class LabProvider<TEncryptor> where TEncryptor : IEncryptor
{
    private readonly TEncryptor _encryptor;

    public LabProvider(string key)
    {
        _encryptor = Activator.CreateInstance<TEncryptor>();
        _encryptor.Key = key;
    }
    
    public string Encrypt(string value) => _encryptor.Encrypt(value);
    public string Decrypt(string value) => _encryptor.Decrypt(value);
}