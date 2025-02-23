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

    public T ElementAt(int row, int column)
    {
        if (row >= Rows || row < 0) throw new ArgumentException($"Індекс рядка має бути 0-{Rows}");
        if (column >= Columns || column < 0) throw new ArgumentException($"Індекс колонки має бути 0-{Columns}");
        return _matrix[row][column];
    }

    public override bool Equals(object? obj)
    {
        if (obj is Matrix<T> matrix)
        {
            if (Rows != matrix.Rows || Columns != matrix.Columns) return false;
            return ToString() == matrix.ToString();
        }

        return false;
    }

    public override string ToString()
    {
        var result = string.Empty;
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Columns; j++)
            {
                var end = j == Columns - 1 ? string.Empty : " ";
                result += ElementAt(i, j) + end;
            }

            if (i != Rows - 1) result += "\n";
        }

        return result;
    }

    private int CalculateColumnsCount()
    {
        var cols = _matrix.First().Length;
        if (_matrix.Any(row => row.Length != cols))
            throw new ArgumentException("Кількість колонок має бути однаковим.");

        return cols;
    }

    private int CalculateRowsCount()
    {
        return _matrix.Length;
    }
}