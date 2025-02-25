namespace Lab2.Common.Math;

public partial class Matrix<T>
{
    public static Matrix<T> operator *(Matrix<T> first, Matrix<T> second)
    {
        var result = new List<T[]>();
        if (first.Columns != second.Rows)
            throw new ArgumentException(
                "Кількість стовпців першої матриці повинна дорівнювати кількості рядків другої матриці.");

        var rows = first.Rows;
        var columns = second.Columns;
        for (var i = 0; i < rows; i++)
        {
            var temp = new List<T>();
            for (var j = 0; j < columns; j++)
            {
                var sum = default(T);
                for (var k = 0; k < second.Rows; k++)
                    if (sum is not null)
                        sum += first.ElementAt(i, k) * second.ElementAt(k, j);

                if (sum is not null) temp.Add(sum);
            }

            result.Add(temp.ToArray());
        }

        return new Matrix<T>(result.ToArray());
    }

    public static Matrix<T> operator *(Matrix<T> first, T second)
    {
        var result = first.Clone();
        for (var i = 0; i < first.Rows; i++)
        for (var j = 0; j < first.Columns; j++)
        {
            var newValue = result.ElementAt(i, j) * second;
            result.Set(i, j, newValue);
        }

        return result;
    }

    public static Matrix<T> operator *(T first, Matrix<T> second)
    {
        return second * first;
    }
}