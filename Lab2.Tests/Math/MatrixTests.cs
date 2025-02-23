using Lab2.Common.Math;

namespace Lab2.Tests.Math;

[TestClass]
public class MatrixTests
{
    private readonly Matrix<int> _matrix = new([[0, 1, 2], [3, 4, 5], [6, 7, 8]]);

    [TestMethod]
    public void ShouldCorrectToString()
    {
        const string expected = "0 1 2\n3 4 5\n6 7 8";
        var result = _matrix.ToString();
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ShouldCorrectRemoveTwoMatrix()
    {
        var expected = new Matrix<int>([[0, 0, 0], [0, 0, 0], [0, 0, 0]]);
        var result = _matrix - _matrix;
        Assert.AreEqual(expected.ToString(), result.ToString());
    }

    [TestMethod]
    public void ShouldCorrectRemoveMatrixAndNumber()
    {
        const int number = 2;
        var expected = new Matrix<int>([[-2, -1, 0], [1, 2, 3], [4, 5, 6]]);
        var result = _matrix - number;
        Assert.AreEqual(expected.ToString(), result.ToString());
    }

    [TestMethod]
    public void ShouldCorrectRemoveNumberAndMatrix()
    {
        const int number = 2;
        var expected = new Matrix<int>([[2, 1, 0], [-1, -2, -3], [-4, -5, -6]]);
        var result = number - _matrix;
        Assert.AreEqual(expected.ToString(), result.ToString());
    }

    [TestMethod]
    public void ShouldCorrectAddTwoMatrix()
    {
        var expected = new Matrix<int>([[0, 2, 4], [6, 8, 10], [12, 14, 16]]);
        var result = _matrix + _matrix;
        Assert.AreEqual(expected.ToString(), result.ToString());
    }

    [TestMethod]
    public void ShouldCorrectAddMatrixAndNumber()
    {
        const int number = 2;
        var expected = new Matrix<int>([[2, 3, 4], [5, 6, 7], [8, 9, 10]]);
        var result = _matrix + number;
        Assert.AreEqual(expected.ToString(), result.ToString());
    }

    [TestMethod]
    public void ShouldCorrectAddNumberAndMatrix()
    {
        const int number = 2;
        var expected = new Matrix<int>([[2, 3, 4], [5, 6, 7], [8, 9, 10]]);
        var result = number + _matrix;
        Assert.AreEqual(expected.ToString(), result.ToString());
    }
}