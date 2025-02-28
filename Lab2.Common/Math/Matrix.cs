using System.Numerics;

namespace Lab2.Common.Math;

public partial class Matrix<T> where T : INumber<T>
{
    private readonly T[][] _matrix;

    public Matrix(T[][] matrix)
    {
        _matrix = matrix;
        Rows = CalculateRowsCount();
        Columns = CalculateColumnsCount();
    }

    public int Columns { get; }
    public int Rows { get; }

    public T Determinant => Determinate();

    public T ElementAt(int row, int column)
    {
        if (row >= Rows || row < 0) throw new ArgumentException($"Індекс рядка має бути 0-{Rows - 1}.");
        if (column >= Columns || column < 0) throw new ArgumentException($"Індекс колонки має бути 0-{Columns - 1}.");
        return _matrix[row][column];
    }

    public Matrix<T> Reverse()
    {
        var determinant = Determinant;
        if (determinant.Equals(T.Zero)) throw new Exception("Матриця ме має оберненої.");
        var reversedDeterminant = T.One / determinant;
        var matrixTemp = new List<T[]>();
        var temp = new List<T>();
        ForEach((row, column, _) =>
        {
            temp.Add(AlgebraicAddition(column, row));
            if (column != Columns - 1) return;
            matrixTemp.Add(temp.ToArray());
            temp.Clear();
        });
        var matrix = new Matrix<T>(matrixTemp.ToArray());
        return matrix * reversedDeterminant;
    }

    private T Determinate()
    {
        if (Rows != Columns) throw new Exception("Матриця має бути квадратна.");
        return Rows switch
        {
            0 => T.One,
            1 => ElementAt(0, 0),
            2 => Determinate2X2(),
            3 => Determinate3X3(),
            _ => DeterminateOfHigherMatrix()
        };
    }

    private T Determinate2X2()
    {
        var first = ElementAt(0, 0) * ElementAt(1, 1);
        var second = ElementAt(0, 1) * ElementAt(1, 0);
        return first - second;
    }

    private T Determinate3X3()
    {
        var first = ElementAt(0, 0) * ElementAt(1, 1) * ElementAt(2, 2);
        var second = ElementAt(1, 0) * ElementAt(2, 1) * ElementAt(0, 2);
        var third = ElementAt(0, 1) * ElementAt(1, 2) * ElementAt(2, 0);
        var fourth = ElementAt(0, 2) * ElementAt(1, 1) * ElementAt(2, 0);
        var fifth = ElementAt(1, 2) * ElementAt(2, 1) * ElementAt(0, 0);
        var sixth = ElementAt(0, 1) * ElementAt(1, 0) * ElementAt(2, 2);
        return first + second + third - fourth - fifth - sixth;
    }

    private T DeterminateOfHigherMatrix()
    {
        var determinant = T.Zero;
        ForEach((row, column, value) => determinant += AlgebraicAddition(row, column) * value);
        return determinant;
    }
}