using System.Numerics;

namespace Lab2.Common.Math;

public class Matrix<T> where T : INumber<T>
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