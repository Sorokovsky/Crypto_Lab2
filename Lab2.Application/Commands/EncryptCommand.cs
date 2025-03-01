using Lab2.Application.Helpers;
using Lab2.Commands.Commands;
using Lab2.Commands.Interfaces;

namespace Lab2.Application.Commands;

public class EncryptCommand : BasicCommand
{
    public override string Title { get; } = "Зашифрувати";

    public override void Invoke(IExitable? exitable)
    {
        var encryptor = EncryptorsHelper.Choose();
        var key = TextHelper.ChooseInput("ключ").Invoke();
        var text = TextHelper.ChooseInput("текст").Invoke();
        var output = encryptor.Encrypt(text, key);
        TextHelper.ChooseOutput("зашифрований текст").Invoke(output);
    }
}