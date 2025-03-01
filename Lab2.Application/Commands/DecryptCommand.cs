using Lab2.Application.Helpers;
using Lab2.Commands.Commands;
using Lab2.Commands.Interfaces;
using Lab2.Common.Interfaces;
using Lab2.Visionary;

namespace Lab2.Application.Commands;

public class DecryptCommand : BasicCommand
{
    public override string Title { get; } = "Дешифрувати";

    public override void Invoke(IExitable? exitable)
    {
        IEncryptor encryptor = new VisionaryEncryptor(CommandsHelper.ChooseAlphabets());
        var key = TextHelper.ChooseInput("ключ").Invoke();
        var text = TextHelper.ChooseInput("текст").Invoke();
        var output = encryptor.Decrypt(text, key);
        TextHelper.ChooseOutput("розшифрований текст").Invoke(output);
    }
}