using Lab2.Commands.Chooser;
using Lab2.Common.Interfaces;
using Lab2.Hill;
using Lab2.Visionary;

namespace Lab2.Application.Helpers;

public static class EncryptorsHelper
{
    private static readonly List<string> Encryptors = ["Криптосистема віженера", "Криптосистема хіла"];

    public static IEncryptor Choose()
    {
        var encryptor = Choosing
            .FromList(Encryptors.Select(x => Encryptors.IndexOf(x)).ToList(),
                s => $"{s}-{Encryptors[s]}",
                int.Parse,
                (s, b) => s == b,
                "криптосистеми"
            );
        if (Encryptors[encryptor] == "Криптосистема віженера")
        {
            var alphabet = CommandsHelper.ChooseAlphabets();
            return new VisionaryEncryptor(alphabet);
        }

        if (Encryptors[encryptor] == "Криптосистема хіла")
        {
            var alphabet = CommandsHelper.ChooseAlphabets();
            var n = Choosing.Number("розмір головної матриці", 0, null);
            return new HillEncryptor(alphabet, n);
        }

        throw new Exception("Криптосистему не знайдено.");
    }
}