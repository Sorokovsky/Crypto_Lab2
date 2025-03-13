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
        return GetNewString(text, key, false);
    }

    public string Decrypt(string text, string key)
    {
        return GetNewString(text, key, true);
    }

    private string GetNewString(string text, string key, bool reversed)
    {
        var mainMatrix = CreateMainMatrix(key);
        if (reversed) mainMatrix = GenerateReversedMatrixForModule(mainMatrix, Alphabet.Letters.Length);
        var sumMatrixes = CreateTextMatrices(text);
        var resultMatrices = sumMatrixes.Select(x => mainMatrix * x).ToList();
        var result = new string(resultMatrices
            .Select(x =>
                {
                    var temp = string.Empty;
                    x.ForEach((_, _, value) =>
                    {
                        var module = Alphabet.Letters.Length;
                        var index = value % module;
                        temp += Alphabet.Letters[index];
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

    private Matrix<int> CreateMainMatrix(string key)
    {
        var j = 0;
        var tempMatrix = new List<int[]>();
        var temp = new List<int>();
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

        return new Matrix<int>(tempMatrix.ToArray());
    }

    private List<Matrix<int>> CreateTextMatrices(string text)
    {
        var letters = text.Where(x => Alphabet.Letters.Contains(char.ToUpper(x))).ToArray();
        if (letters.Length % _n != 0)
            throw new ArgumentException("Довжина тексту(кількість літер) має ділитися на розмір матриці.");
        var result = new List<Matrix<int>>();
        var i = 0;
        var tempMatrix = new List<int[]>();
        foreach (var index in letters.Select(symbol => Alphabet.Letters.IndexOf(char.ToUpper(symbol))))
        {
            tempMatrix.Add([index]);
            i++;
            if (i != _n) continue;
            i = 0;
            result.Add(new Matrix<int>(tempMatrix.ToArray()));
            tempMatrix.Clear();
        }

        return result;
    }

    private static Matrix<int> GenerateReversedMatrixForModule(Matrix<int> matrix, int module)
    {
        var determinate = matrix.Determinant;
        for (var i = 0; i < module; i++)
        {
            if (i * determinate % module != 1) continue;
            determinate = i;
            break;
        }

        var matrixTemp = new List<int[]>();
        var temp = new List<int>();
        matrix.ForEach((row, column, _) =>
        {
            var result = matrix.AlgebraicAddition(column, row) * determinate;
            while (result < 0) result += module;

            temp.Add((result + module) % module);
            if (column == matrix.Columns - 1)
            {
                matrixTemp.Add(temp.ToArray());
                temp.Clear();
            }
        });
        return new Matrix<int>(matrixTemp.ToArray());
    }
}