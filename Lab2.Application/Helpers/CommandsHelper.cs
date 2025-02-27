using Lab2.Commands.Chooser;
using Lab2.Common.Tools;

namespace Lab2.Application.Helpers;

public static class CommandsHelper
{
    public static (string Letters, double Frequencies) ChooseAlphabets()
    {
        return Choosing.FromList(
            Alphabets.All.ToList(),
            x => x.Key,
            x => x,
            (pair, s) => pair.Key == s,
            "Виберіть абетку"
        ).Value;
    }
}