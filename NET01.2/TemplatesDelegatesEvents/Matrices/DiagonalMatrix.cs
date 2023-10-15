using TemplatesDelegatesEvents.Helpers;

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
        }

        public override T this[int row, int column]
        {
            get => (row != column ? Data[row] : default)!;
            set
            {
                if (row == column)
                {
                    var oldValue = Data[row];
                    if (Equals(oldValue, value)) return;
                    Data[row] = value;
                    OnElementChanged(new EventMatrixElementChangedArgs<T>(row, row, oldValue, value));
                }
                else
                {
                    throw new ArgumentException("You can't change these element in diagonal matrix!");
                }
            }
        }

    }
}
