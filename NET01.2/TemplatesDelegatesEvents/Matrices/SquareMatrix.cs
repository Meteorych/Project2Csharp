using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TemplatesDelegatesEvents.Matrices
{
    /// <summary>
    /// Parent-class for realization of matrix
    /// </summary>
    internal class SquareMatrix<T>
    {
        protected T[] Data;
        public int Size { get; }
        //Creating the delegate Event Handler, that will contain references to events
        public event EventHandler<EventMatrixElementChangedArgs<T>> ElementChanged;
        public SquareMatrix(int size)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "Size should be natural.");
            Size = size;
            Data = new T[Size * Size];
            ElementChanged += (sender, e) => { };
        }
        /// <summary>
        /// Indexer that give users opportunity to work with definite element by writing its indexes.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public T this[int row, int col] 
        {
            get 
            {
                if (row < 0 || col < 0 || row > Size || col > Size) throw new ArgumentOutOfRangeException(nameof(row), "Wrong indices.");
                return Data[row * Size + col];
            }

            set
            {
                if (row < 0 || col < 0 || row > Size || col > Size) throw new ArgumentOutOfRangeException(nameof(row), "Wrong indices.");
                T oldValue = Data[row * Size + col];
                if (!Equals(oldValue, value))
                {
                    Data[row * Size + col] = value;
                    ElementChanged?.Invoke(this, new EventMatrixElementChangedArgs<T>(row, col, oldValue, value));
                }
            }
        } 
    }
   
}
