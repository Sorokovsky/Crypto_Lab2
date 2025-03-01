using Lab2.Commands.Chooser;
using Lab2.Common.Interfaces;
using Lab2.Hill;
using Lab2.Visionary;

namespace Lab2.Application.Helpers;

public static class EncryptorsHelper
{
    private static readonly List<string> Encryptors = ["Криптосистема Віженера", "Криптосистема Хіла"];

    public static IEncryptor Choose()
    {
        var encryptor = Choosing
            .FromList(Encryptors,
                s => $"{Encryptors.IndexOf(s)}-{s}.",
                int.Parse,
                (s, b) => Encryptors.IndexOf(s) == b,
                "криптосистеми"
            );
        if (encryptor == "Криптосистема Віженера")
        {
            var alphabet = CommandsHelper.ChooseAlphabets();
            return new VisionaryEncryptor(alphabet);
        }

        if (encryptor == "Криптосистема Хіла")
        {
            var alphabet = CommandsHelper.ChooseAlphabets();
            var n = Choosing.Number("розмір головної матриці", 0, null);
            return new HillEncryptor(alphabet, n);
        }

        throw new Exception("Криптосистему не знайдено.");
    }
}