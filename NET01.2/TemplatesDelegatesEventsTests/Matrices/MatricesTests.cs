using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplatesDelegatesEvents.Matrices;

namespace TemplatesDelegatesEventsTests.Matrices
{
    [TestClass]
    public class MatricesTests
    {
        [TestMethod]
        public void Square_Matrix_Setter_Getter_Test()
        {
            const int expectedResult = 1;
            SquareMatrix<int> testSquareMatrix = new(3)
            {
                [0,1] = expectedResult
            };
            Assert.AreEqual(testSquareMatrix[0, 1], expectedResult);
        }
        [TestMethod]
        public void Diagonal_Matrix_Getter_Test()
        {
            DiagonalMatrix<int> testDiagonalMatrix = new (3);
            Assert.AreEqual(default, testDiagonalMatrix[0, 1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Diagonal_Matrix_Setter_Test()
        {
            DiagonalMatrix<int> testDiagonalMatrix = new(3)
            {
                [0, 1] = 3
            };
        }
        [TestMethod]
        public void Set_Element_Value_Event_Matrix_Test()
        {
            const int expectedRow = 0;
            const int expectedColumn = 1;
            const int expectedOldValue = 3;
            const int expectedNewValue = 5;
            var eventRaised = false;
            SquareMatrix<int> testMatrix = new(5)
            {
                [0, 1] = 3
            };
            testMatrix.ElementChanged += (s, e) =>
            {
                Assert.AreEqual(expectedRow, e.Row);
                Assert.AreEqual(expectedColumn, e.Col);
                Assert.AreEqual(expectedOldValue, e.OldValue);
                Assert.AreEqual(expectedNewValue, e.NewValue);
                eventRaised = true;
            };
            testMatrix[0, 1] = 5;
            Assert.IsTrue(eventRaised);
        }
        [TestMethod]
        public void Set_Same_Element_Value_Event_Matrix_Test()
        {
            var eventRaised = false;
            DiagonalMatrix<int> testMatrix = new(5)
            {
                [0, 0] = 3
            };
            testMatrix.ElementChanged += (s, e) =>
            {
                eventRaised = true;
            };
            testMatrix[0, 0] = 3;
            Assert.IsFalse(eventRaised);
        }
    }
}