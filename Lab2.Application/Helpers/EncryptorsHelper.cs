using Lab2.Commands.Chooser;
using Lab2.Common.Interfaces;
using Lab2.Hill;
using Lab2.Visionary;

namespace Lab2.Application.Helpers;

public static class EncryptorsHelper
{
    private static readonly IReadOnlyDictionary<string, Func<IEncryptor>> Encryptors =
        new Dictionary<string, Func<IEncryptor>>
        {
            {
                "Криптосистема Віженера",
                BuildVisionaryEncryptor
            },
            {
                "Криптосистема Хіла",
                BuildHillEncryptor
            }
        };

    public static IEncryptor Choose()
    {
        var list = Encryptors.ToList();
        return Choosing
            .FromList(list,
                s => $"{list.IndexOf(s)}-{s.Key}.",
                int.Parse,
                (s, b) => list.IndexOf(s) == b,
                "криптосистеми"
            ).Value();
    }

    private static IEncryptor BuildVisionaryEncryptor()
    {
        var alphabet = CommandsHelper.ChooseAlphabets();
        return new VisionaryEncryptor(alphabet);
    }

    private static IEncryptor BuildHillEncryptor()
    {
        var alphabet = CommandsHelper.ChooseAlphabets();
        var n = Choosing.Number("розмір головної матриці", 0, null);
        return new HillEncryptor(alphabet, n);
    }
}