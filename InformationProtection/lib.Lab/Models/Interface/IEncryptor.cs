namespace lib.Lab.Models.Interface;

public interface IEncryptor
{
    /// <summary>
    /// Ключ для шифрования и дешифрования
    /// </summary>
    public string Key { get; set; }
    /// <summary>
    /// Метод шифрвоания
    /// </summary>
    /// <param name="value"></param>
    /// <returns>text encrypt</returns>
    public string Encrypt(string value);
    /// <summary>
    /// Метод расшифрования
    /// </summary>
    /// <param name="value"></param>
    /// <returns>text decrypt</returns>
    public string Decrypt(string value);
}