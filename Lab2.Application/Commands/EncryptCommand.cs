using Lab2.Commands.Chooser;
using Lab2.Commands.Commands;
using Lab2.Commands.Interfaces;
using Lab2.Common.Interfaces;
using Lab2.Common.Tools;
using Lab2.Visionary;

namespace Lab2.Application.Commands;

public class EncryptCommand : BasicCommand
{
    public override string Title { get; } = "Зашифрувати";

    public override void Invoke(IExitable? exitable)
    {
        var alphabet = Choosing.FromList(
            Alphabets.All.ToList(),
            x => x.Key,
            x => x,
            (pair, s) => pair.Key == s,
            "Виберіть абетку"
        );
        IEncryptor encryptor = new VisionaryEncryptor(alphabet.Value);
        var key = Choosing.Text("ключ");
        var text = Choosing.Text("текст");
        var output = encryptor.Encrypt(text, key);
        Console.WriteLine($"Зашифрований текст: \"{output}\"");
    }
}