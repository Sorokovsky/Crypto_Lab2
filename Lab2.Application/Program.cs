using Lab2.Common.Analysing;
using Lab2.Common.Interfaces;
using Lab2.Visionary;

namespace Lab2.Application;

public static class Program
{
    public static void Main()
    {
        const string text =
            "Mrgf niatxz qv ffnux ff ybt ce tyx iix gzka cjlrgk qyeix oy yau apx yij lhprgv tsfp a ynny urzophx wyxlf rnu t zbrfk ahfw fzesyu wzmo llbsbzb jhfplx khv ivmztzhu iw ae tiuedfglx diex iyjiux pn neix abv cintvciez yydazgz iw tyx jiktrzlm ff kal gznvkz xiimxu unap gv xfusm is khv yvoc rvxr iw tyx zoirfnuxznx ldudp zg vhv ow moyje r lauglv tux thrbu qzty t xornkbas ff xghqvd shuyj syhdyu wyxyy khv tucdac ahx sevg jiefzglx r sbxsykoe ppny a ktuace fy ilfwe ahci auallznx mv ck lrr hgfnx moy ueskpm.";
        var output = CypherAnalyzer.TryHack(text, new VisionaryEncryptor(), Choosing);
        Console.WriteLine(output);
    }

    private static string Choosing(IEncryptor encryptor, string text)
    {
        var result = string.Empty;
        while (true)
            try
            {
                Console.WriteLine("Виберіть з кожного рядка по літері і напишіть в 1 рядок.");
                Console.Write(">> ");
                var key = Console.ReadLine() ?? string.Empty;
                result = encryptor.Decrypt(text, key);
                Console.WriteLine($"З ключем: {key}.");
                Console.WriteLine($"Вийшло: \"{result}\"");
                Console.WriteLine("Ви задоволені (0-Так, 1-Ні): ");
                var operation = Console.ReadLine() ?? string.Empty;
                if (operation == "0") break;
                if (operation != "1") throw new Exception("Відповідь не розпізнано.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Спробуйте ше.");
                throw;
            }

        return result;
    }
}