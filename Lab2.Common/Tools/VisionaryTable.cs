namespace Lab2.Common.Tools;

public static class VisionaryTable
{
    public static List<List<char>> Generate(string alphabet = Alphabets.En)
    {
        var result = new List<List<char>>();
        for (var i = 0; i < alphabet.Length; i++)
        {
            var row = new List<char>();
            for (var j = 0; j < alphabet.Length; j++)
            {
                var letter = alphabet[(i + j + alphabet.Length) % alphabet.Length];
                row.Add(letter);
            }

            result.Add(row);
        }

        return result;
    }
}