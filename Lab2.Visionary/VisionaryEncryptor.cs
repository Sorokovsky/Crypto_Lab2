using Lab2.Common.Interfaces;
using Lab2.Common.Tools;

namespace Lab2.Visionary;

public class VisionaryEncryptor : IEncryptor
{
    private readonly string _alphabet;
    private readonly VisionaryTable _table;

    public VisionaryEncryptor(string alphabet = Alphabets.En)
    {
        _alphabet = alphabet;
        _table = VisionaryTable.Generate(alphabet);
    }

    public string Encrypt(string text, string key)
    {
        return GetNewString(text, key, GetEncryptedLetter);
    }

    public string Decrypt(string text, string key)
    {
        return GetNewString(text, key, GetDecryptedLetter);
    }

    private static string ResizeKey(string key, int count)
    {
        key = key.Trim();
        while (key.Length < count) key += key;

        key = key[..count];
        return key;
    }

    private string GetNewString(string text, string key, Func<char, char, char> getNewLetter)
    {
        key = ResizeKey(key, text.Length);
        var result = string.Empty;
        for (int i = 0, j = 0; i < text.Length; i++, j++)
        {
            var textLetter = text[i];
            var isLetter = _alphabet.IndexOf(char.ToUpper(textLetter)) != -1;
            var keyLetter = key[j];
            if (isLetter)
            {
                var newLetter = getNewLetter(textLetter, keyLetter);
                if (char.IsLower(textLetter)) newLetter = char.ToLower(newLetter);
                result += newLetter;
            }
            else
            {
                result += textLetter;
                j--;
            }
        }

        return result;
    }

    private char GetEncryptedLetter(char text, char key)
    {
        var tableKey = new VisionaryKey(key, text);
        return _table.GetByKey(tableKey);
    }

    private char GetDecryptedLetter(char text, char key)
    {
        var letter = ' ';
        for (var i = 0; i < _alphabet.Length; i++)
        {
            letter = _alphabet[i];
            var output = _table.GetByKey(new VisionaryKey(key, letter));
            if (output == char.ToUpper(text)) break;
        }

        return letter;
    }
}