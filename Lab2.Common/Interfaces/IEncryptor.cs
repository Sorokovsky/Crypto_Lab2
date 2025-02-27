namespace Lab2.Common.Interfaces;

public interface IEncryptor
{
    public (string Letters, double Frequencies) Alphabet { get; }

    public string Encrypt(string text, string key);

    public string Decrypt(string text, string key);
}