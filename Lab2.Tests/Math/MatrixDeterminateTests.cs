using Lab2.Common.Math;

namespace Lab2.Tests.Math;

[TestClass]
public class MatrixDeterminantTests
{
    [TestMethod]
    public void SuccessDeterminantOf3X3Matrix()
    {
        var matrix = new Matrix<double>([
            [2, 1, 3],
            [1, 2, 1],
            [3, 2, 1]
        ]);

        var result = matrix.Determinant;
        const int expected = -10;
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SuccessDeterminantOf2X2Matrix()
    {
        var matrix = new Matrix<double>([
            [4, 3],
            [6, 3]
        ]);

        var result = matrix.Determinant;
        const double expected = -6;
        Assert.AreEqual(expected, result, 1e-5);
    }

    [TestMethod]
    public void SuccessDeterminantOfHigherMatrix()
    {
        var matrix = new Matrix<double>([
            [1, 2, 3, 4],
            [5, 6, 7, 8],
            [9, 8, 7, 6],
            [5, 4, 3, 2]
        ]);
        const double expected = 0;
        var result = matrix.Determinant;
        Assert.AreEqual(expected, result);
    }
}