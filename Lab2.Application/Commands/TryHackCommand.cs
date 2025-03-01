using Lab2.Application.Helpers;
using Lab2.Commands.Chooser;
using Lab2.Commands.Commands;
using Lab2.Commands.Interfaces;
using Lab2.Common.Analysing;

namespace Lab2.Application.Commands;

public class TryHackCommand : BasicCommand
{
    public override string Title { get; } = "Спробувати взломати шифр";

    public override void Invoke(IExitable? exitable)
    {
        var text = TextHelper.ChooseInput("текст").Invoke();
        var encryptor = EncryptorsHelper.Choose();
        TextHelper.ChooseOutput("розшифрований текст")
            .Invoke(CypherAnalyzer.TryHack(text, encryptor, EnterKey, EnterIsNotContinue));
    }

    private static string EnterKey()
    {
        return Choosing.Text("ключ, який складається з кожного літери з рядка");
    }

    private static bool EnterIsNotContinue()
    {
        return Choosing.Binary("Задоволені ключем");
    }
}