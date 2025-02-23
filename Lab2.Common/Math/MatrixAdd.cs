using System.Numerics;

namespace Lab2.Common.Math;

public partial class Matrix<T> where T : INumber<T>
{
    public static Matrix<T> operator +(Matrix<T> first, Matrix<T> second)
    {
        if (first.Rows != second.Rows) throw new ArgumentException("Кількість рядків має співпадати.");
        if (first.Columns != second.Columns) throw new ArgumentException("Кількість стовпців має співпадати.");
        var result = new List<T[]>();
        for (var i = 0; i < first.Rows; i++)
        {
            var temp = new List<T>();
            for (var j = 0; j < first.Columns; j++) temp.Add(first.ElementAt(i, j) + second.ElementAt(i, j));

            result.Add(temp.ToArray());
        }

        return new Matrix<T>(result.ToArray());
    }

    public static Matrix<T> operator +(Matrix<T> first, T second)
    {
        var secondMatrix = new List<T[]>();
        for (var i = 0; i < first.Rows; i++)
        {
            var temp = new List<T>();
            for (var j = 0; j < first.Columns; j++) temp.Add(second);

            secondMatrix.Add(temp.ToArray());
        }

        return first + new Matrix<T>(secondMatrix.ToArray());
    }

    public static Matrix<T> operator +(T first, Matrix<T> second)
    {
        return second + first;
    }
}