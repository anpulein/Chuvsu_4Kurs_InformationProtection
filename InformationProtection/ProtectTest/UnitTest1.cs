using lib.Lab.Controllers.Lab4;
using lib.Lab.Controllers.Lab5;
using lib.Lab.Models.Interface;

namespace ProtectTest;

public class Tests
{
    
    private string? _key;
    private string? _message;
    
    [SetUp]
    public void Setup()
    {
        _key = "fghabwyh";
        _message = "Hello my name is Dima!";
        // _key = "11111111";
        // _message = "Hello, hello dshfsdhfhsdkf sdf sdfs";
    }

    [Test]
    public void DesEcb()
    {
        // Check (_key, _message) is not null
        if (_key != null && _message != null)
        {
            var result = Encryption(new DesEcbEncryptor(_key), _message);
            
            Assert.That(result, Is.EqualTo(_message));
        }
        else
        {
            Assert.Fail("(_key, _message) is not initialized.");
        }
    }

    [Test]
    public void DesCbc()
    {
        // Check (_key, _message) is not null
        if (_key != null && _message != null)
        {
            var result = Encryption(new DesCbcEncryptor(_key), _message);
            
            Assert.That(result, Is.EqualTo(_message));
        }
        else
        {
            Assert.Fail("(_key, _message) is not initialized.");
        }
    }
    
    [Test]
    public void DesCfb()
    {
        // Check (_key, _message) is not null
        if (_key != null && _message != null)
        {
            var result = Encryption(new DesCfbEncryptor(_key), _message);
            
            Assert.That(result, Is.EqualTo(_message));
        }
        else
        {
            Assert.Fail("(_key, _message) is not initialized.");
        }
    }
    
    [Test]
    public void DesOfb()
    {
        // Check (_key, _message) is not null
        if (_key != null && _message != null)
        {
            var result = Encryption(new DesOfbEncryptor(_key), _message);
            
            Assert.That(result, Is.EqualTo(_message));
        }
        else
        {
            Assert.Fail("(_key, _message) is not initialized.");
        }
    }


    private string Encryption(IEncryptor encryptor, string message)
    {
        var enc = encryptor.EncryptTest(message);
        var dec = encryptor.DecryptTest(enc);

        return dec.TrimEnd();
    }
}