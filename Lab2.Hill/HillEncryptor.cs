using Lab2.Common.Interfaces;
using Lab2.Common.Math;

namespace Lab2.Hill;

public class HillEncryptor : IEncryptor
{
    private readonly int _n;

    public HillEncryptor((string Letters, double Frequencies) alphabet, int n)
    {
        Alphabet = alphabet;
        _n = n;
    }

    public (string Letters, double Frequencies) Alphabet { get; }

    public string Encrypt(string text, string key)
    {
        var mainMatrix = CreateMainMatrix(key);
        var sumMatrixes = CreateTextMatrices(text);
        var resultMatrices = sumMatrixes.Select(x => mainMatrix * x).ToList();
        var result = new string(resultMatrices
            .Select(x =>
                {
                    var temp = string.Empty;
                    x.ForEach((row, column, value) =>
                    {
                        temp += Alphabet.Letters[(int)Math.Round(value) % Alphabet.Letters.Length];
                    });
                    return temp.ToArray();
                }
            )
            .Aggregate(new char[] { }, (chars1, chars2) =>
            {
                var tempChars = new List<char>();
                chars2.ToList().ForEach(tempChars.Add);
                return chars1.Concat(tempChars).ToArray();
            }));
        return result;
    }

    public string Decrypt(string text, string key)
    {
        throw new NotImplementedException();
    }

    private Matrix<double> CreateMainMatrix(string key)
    {
        var j = 0;
        var tempMatrix = new List<double[]>();
        var temp = new List<double>();
        foreach (var symbol in key)
        {
            var index = Alphabet.Letters.IndexOf(char.ToUpper(symbol));
            if (index == -1) continue;
            temp.Add(index);
            j++;
            if (j != _n) continue;
            j = 0;
            tempMatrix.Add(temp.ToArray());
            temp = [];
        }

        return new Matrix<double>(tempMatrix.ToArray());
    }

    private List<Matrix<double>> CreateTextMatrices(string text)
    {
        var letters = text.Where(x => Alphabet.Letters.Contains(char.ToUpper(x))).ToArray();
        if (letters.Length % _n != 0)
            throw new ArgumentException("Довжина тексту(кількість літер) має ділитися на розмір матриці.");
        var result = new List<Matrix<double>>();
        var i = 0;
        var tempMatrix = new List<double[]>();
        foreach (var index in letters.Select(symbol => Alphabet.Letters.IndexOf(char.ToUpper(symbol))))
        {
            tempMatrix.Add([index]);
            i++;
            if (i != _n) continue;
            i = 0;
            result.Add(new Matrix<double>(tempMatrix.ToArray()));
            tempMatrix.Clear();
        }

        return result;
    }
}