using Lab2.Common.Math;

namespace Lab2.Tests.Math;

[TestClass]
public class MatrixDeterminantTests
{
    [TestMethod]
    public void TestDeterminantOf3X3Matrix()
    {
        var matrix = new Matrix<double>([
            [1, 2, 3],
            [0, 4, 5],
            [1, 0, 6]
        ]);

        var result = matrix.Determinant;
        const int expected = 22;
        Assert.AreEqual(expected, result, 1e-5);
    }

    [TestMethod]
    public void Test_Determinant_Of_Identity_Matrix()
    {
        var matrix = new Matrix<double>([
            [1, 0, 0],
            [0, 1, 0],
            [0, 0, 1]
        ]);

        var result = matrix.Determinant;
        const int expected = 1;
        Assert.AreEqual(expected, result, 1e-5);
    }

    [TestMethod]
    public void Test_Determinant_Of_Singular_Matrix()
    {
        var matrix = new Matrix<double>([
            [2, 4, 2],
            [1, 2, 1],
            [3, 6, 3]
        ]);

        var result = matrix.Determinant;
        const int expected = 0;
        Assert.AreEqual(expected, result, 1e-5);
    }

    [TestMethod]
    public void Test_Determinant_Of_2x2_Matrix()
    {
        var matrix = new Matrix<double>([
            [4, 3],
            [6, 3]
        ]);

        var result = matrix.Determinant;
        const double expected = -6;
        Assert.AreEqual(expected, result, 1e-5);
    }
}