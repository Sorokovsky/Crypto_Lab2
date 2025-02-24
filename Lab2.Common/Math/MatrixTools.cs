namespace Lab2.Common.Math;

public partial class Matrix<T>
{
    public override bool Equals(object? obj)
    {
        if (obj is Matrix<T> matrix)
        {
            if (Rows != matrix.Rows || Columns != matrix.Columns) return false;
            return ToString() == matrix.ToString();
        }

        return false;
    }

    public void Set(int row, int column, T value)
    {
        if (row >= Rows || row < 0 || column >= Columns || column < 0)
            throw new ArgumentException("Індекси за межами.");

        _matrix[row][column] = value;
    }

    public static Matrix<T> FromOnce(T item, int rows, int columns)
    {
        var matrix = new List<T[]>();
        for (var i = 0; i < rows; i++)
        {
            var temp = new List<T>();
            for (var j = 0; j < columns; j++) temp.Add(item);

            matrix.Add(temp.ToArray());
        }

        return new Matrix<T>(matrix.ToArray());
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

    private void SwapRows(int row1, int row2)
    {
        if (row1 < 0 || row1 >= Rows || row2 < 0 || row2 >= Rows)
            throw new ArgumentException("Один з індексів за межами.");

        for (var i = 0; i < Columns; i++)
        {
            var temp = ElementAt(row1, i);
            Set(row1, i, ElementAt(row2, i));
            Set(row2, i, temp);
        }
    }

    private Matrix<T> Clone()
    {
        var result = new List<T[]>();
        for (var i = 0; i < Rows; i++)
        {
            var temp = new List<T>();
            for (var j = 0; j < Columns; j++) temp.Add(ElementAt(i, j));

            result.Add(temp.ToArray());
        }

        return new Matrix<T>(result.ToArray());
    }
}