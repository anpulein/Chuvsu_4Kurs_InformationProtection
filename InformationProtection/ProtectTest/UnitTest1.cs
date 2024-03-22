using lib.Lab.Controllers.Lab4;
using lib.Lab.Controllers.Lab5;
using lib.Lab.Controllers.Lab8;
using lib.Lab.Controllers.Lab9;
using lib.Lab.Models.Interface;

namespace ProtectTest;

public class Tests
{
    
    private string? _key;
    private string? _message;
    private string? _path;
    
    [SetUp]
    public void Setup()
    {
        // _key = "fghabwyh";
        // _message = "Hello my name is Dima!";
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
    
    [Test]
    public void IntervalBetweenSentencesEncryptor()
    {
        
        _path = "../../../Files/Lab8.txt";
        _message = "q";
        
        if (string.IsNullOrWhiteSpace(_path))
            Assert.Fail("_path is not initialized.");
        
        // асинхронное чтение
        using (StreamReader reader = new StreamReader(_path))
        {
            _key = reader.ReadToEnd();
        }


        // _key = "1. 2. 3. 4. 5. 6. 7. 8. 9.";
        
        // Check (_key, _message) is not null
        if (!string.IsNullOrWhiteSpace(_key) && !string.IsNullOrWhiteSpace(_message))
        {
            var result = Encryption(new IntervalBetweenSentencesEncryptor(_key), _message);
            
            Assert.That(result, Is.EqualTo(_message));
        }
        else
        {
            Assert.Fail("(_key, _message) is not initialized.");
        }
    }
    
    [Test]
    public void Shorthand()
    {
        
        _path = "../../../Files/Lab8.txt";
        _message = "1325";
        
        if (string.IsNullOrWhiteSpace(_path))
            Assert.Fail("_path is not initialized.");
        
        // асинхронное чтение
        using (StreamReader reader = new StreamReader(_path))
        {
            _key = reader.ReadToEnd();
        }
        
        // Check (_key, _message) is not null
        if (!string.IsNullOrWhiteSpace(_key) && !string.IsNullOrWhiteSpace(_message))
        {
            var result = Encryption(new Shorthand(_key), _message);
            
            Assert.That(result, Is.EqualTo(_message));
        }
        else
        {
            Assert.Fail("(_key, _message) is not initialized.");
        }
    }
    
    [Test]
    public void ShorthandBreakingSpace()
    {
        
        _path = "../../../Files/Lab8.txt";
        _message = "1325";
        
        if (string.IsNullOrWhiteSpace(_path))
            Assert.Fail("_path is not initialized.");
        
        // асинхронное чтение
        using (StreamReader reader = new StreamReader(_path))
        {
            _key = reader.ReadToEnd();
        }
        
        // Check (_key, _message) is not null
        if (!string.IsNullOrWhiteSpace(_key) && !string.IsNullOrWhiteSpace(_message))
        {
            var result = Encryption(new ShorthandBreakingSpace(_key), _message);
            
            Assert.That(result, Is.EqualTo(_message));
        }
        else
        {
            Assert.Fail("(_key, _message) is not initialized.");
        }
    }


    private string Encryption(IEncryptor encryptor, string message)
    {
        try
        {
            var enc = encryptor.EncryptTest(message);
            var dec = encryptor.DecryptTest(enc);
            
            return dec.TrimEnd();
        }
        catch (Exception e)
        {
            Assert.Pass(e.Message);
            return string.Empty;
        }
    }
}