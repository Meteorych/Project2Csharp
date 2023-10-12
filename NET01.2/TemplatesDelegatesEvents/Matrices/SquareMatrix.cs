namespace TemplatesDelegatesEvents.Matrices
{
    /// <summary>
    /// Parent-class for realization of matrix
    /// </summary>
    public class SquareMatrix<T>
    {
        protected T[] Data;
        public int Dimension { get; }
        //Creating the delegate Event Handler, that will contain references to events
        public event EventHandler<EventMatrixElementChangedArgs<T>>? ElementChanged;
        public SquareMatrix(int dimension)
        {
            if (dimension <= 0) throw new ArgumentOutOfRangeException(nameof(dimension), "Dimension should be natural.");
            Dimension = dimension;
            Data = new T[Dimension * Dimension];
        }
        /// <summary>
        /// Indexer that give users opportunity to work with definite element by writing its indexes.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public virtual T this[int row, int col] 
        {
            get 
            {
                if (row < 0 || col < 0 || row > Dimension || col > Dimension)
                {
                    throw new ArgumentOutOfRangeException(nameof(row), "Wrong indices.");
                }
                return Data[row * Dimension + col];
            }

            set
            {
                if (row < 0 || col < 0 || row > Dimension || col > Dimension) throw new ArgumentOutOfRangeException(nameof(row), "Wrong indices.");
                var oldValue = Data[row * Dimension + col];
                if (Equals(oldValue, value)) return;
                Data[row * Dimension + col] = value;
                ElementChanged?.Invoke(this, new EventMatrixElementChangedArgs<T>(row, col, oldValue, value));
            }
        } 
    }
   
}
