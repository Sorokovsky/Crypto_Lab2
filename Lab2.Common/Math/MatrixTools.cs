namespace Lab2.Common.Math;

public partial class Matrix<T>
{
    public override bool Equals(object? obj)
    {
        if (obj is Matrix<T> matrix) return Equals(matrix);
        return false;
    }

    private bool Equals(Matrix<T> matrix)
    {
        if (Rows != matrix.Rows || Columns != matrix.Columns) return false;
        return ToString() == matrix.ToString();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_matrix, Columns, Rows);
    }

    private void ForEach(EachDelegate<T> callback)
    {
        for (var row = 0; row < Rows; row++)
        for (var column = 0; column < Columns; column++)
            callback.Invoke(row, column, ElementAt(row, column));
    }

    private void Set(int row, int column, T value)
    {
        if (row >= Rows || row < 0 || column >= Columns || column < 0)
            throw new ArgumentException("Індекси за межами.");

        _matrix[row][column] = value;
    }

    private static Matrix<T> FromOnce(T item, int rows, int columns)
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

    public Matrix<T> Transpose()
    {
        if (Rows != Columns) throw new Exception("Матриця має бути квадаратна.");
        if (Determinant == T.Zero) throw new Exception("Визначник не має бути нулевим.");
        var result = new List<T[]>();
        var temp = new List<T>();
        ForEach((row, column, _) =>
        {
            temp.Add(ElementAt(column, row));
            if (column != Columns - 1) return;
            result.Add(temp.ToArray());
            temp = [];
        });

        return new Matrix<T>(result.ToArray());
    }

    public T AlgebraicAddition(int row, int column)
    {
        if (row < 0 || row >= Rows || column < 0 || column >= Columns)
            throw new ArgumentException("Індекси за межами.");

        var matrix = DeleteSection(row, column);
        var left = -T.One;
        var power = row + 1 + column + 1;
        for (var i = 0; i <= power; i++) left *= -T.One;

        var right = matrix.Determinant;
        return left * right;
    }

    public override string ToString()
    {
        var result = string.Empty;
        ForEach((row, column, value) =>
        {
            var end = column == Columns - 1 ? row == Rows - 1 ? string.Empty : "\n" : " ";
            result += value + end;
        });

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

    private Matrix<T> DeleteSection(int row, int column)
    {
        if (row < 0 || row >= Rows || column < 0 || column >= Columns)
            throw new ArgumentException("Індекси за межами.");

        var matrix = new List<T[]>();
        List<T> temp = [];
        ForEach((i, j, value) =>
        {
            if (i == row || j == column) return;
            temp.Add(value);
            if (j != Columns - 1) return;
            matrix.Add(temp.ToArray());
            temp = [];
        });

        return new Matrix<T>(matrix.ToArray());
    }

    private Matrix<T> Clone()
    {
        var result = new List<T[]>();
        var temp = new List<T>();
        ForEach((_, column, value) =>
        {
            temp.Add(value);
            if (column != Columns - 1) return;
            result.Add(temp.ToArray());
            temp = [];
        });

        return new Matrix<T>(result.ToArray());
    }

    private delegate void EachDelegate<in TV>(int row, int column, TV value);
}