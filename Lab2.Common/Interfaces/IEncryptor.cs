namespace Lab2.Common.Interfaces;

public interface IEncryptor
{
    public string Encrypt(string text, string key);

    public string Decrypt(string text, string key);
}