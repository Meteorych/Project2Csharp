using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace TemplatesDelegatesEvents.Matrices
{
    /// <summary>
    /// Derived class for creating model of Diagonal Matrix on the base of Square Matrix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DiagonalMatrix<T> : SquareMatrix<T>
    {
        public DiagonalMatrix(int dimension) : base(dimension)
        {
            Data = new T[dimension];
            //Модульные тесты
            for (int i = 0; i < Dimension; i++) 
            {
                Data[(dimension + 1) * i] = default!;
            }
        }

        public override T this[int row, int column]
        {
            get => (row != column ? Data[row] : default)!;
            set
            {
                if (row == column)
                {
                    Data[row] = value;
                }
                else
                {
                    throw new ArgumentException("You can't change these element in diagonal matrix!");
                }
            }
        }

    }
}
