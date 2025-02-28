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
        return new Matrix<T>(matrixTemp.ToArray()) * reversedDeterminant;
    }

    private T Determinate()
    {
        if (Rows != Columns) throw new Exception("Матриця має бути квадратна.");
        var det = T.One;
        var swaps = 0;
        var temp = Clone();
        var tolerance = T.One / (T)Convert.ChangeType(1e9, typeof(T));

        for (var column = 0; column < Columns; column++)
        {
            var pivotRow = column;
            var maxVal = T.Abs(temp.ElementAt(pivotRow, column));

            for (var row = column + 1; row < Rows; row++)
            {
                var val = T.Abs(temp.ElementAt(row, column));
                if (val > maxVal)
                {
                    pivotRow = row;
                    maxVal = val;
                }
            }

            if (maxVal < tolerance) return T.Zero;

            if (pivotRow != column)
            {
                temp.SwapRows(column, pivotRow);
                swaps++;
            }

            for (var row = column + 1; row < Rows; row++)
            {
                var factor = temp.ElementAt(row, column) / temp.ElementAt(column, column);
                for (var j = column; j < Columns; j++)
                {
                    var old = temp.ElementAt(row, j);
                    var value = old - factor * temp.ElementAt(column, j);
                    temp.Set(row, j, value);
                }
            }

            det *= temp.ElementAt(column, column);
        }

        return swaps % 2 == 0 ? det : -det;
    }
}