using Lab2.Application.Helpers;
using Lab2.Commands.Commands;
using Lab2.Commands.Interfaces;

namespace Lab2.Application.Commands;

public class DecryptCommand : BasicCommand
{
    public override string Title { get; } = "Дешифрувати";

    public override void Invoke(IExitable? exitable)
    {
        var encryptor = EncryptorsHelper.Choose();
        var key = TextHelper.ChooseInput("ключ").Invoke();
        var text = TextHelper.ChooseInput("текст").Invoke();
        var output = encryptor.Decrypt(text, key);
        TextHelper.ChooseOutput("розшифрований текст").Invoke(output);
    }
}