using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TemplatesDelegatesEvents.Matrixes
{
    /// <summary>
    /// Parent-class for realization of matrix
    /// </summary>
    abstract class Matrix<T>
    {
        protected T[] _data;
        public int Size { get; }
        public Matrix(int size)
        {
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size), "Size should be natural.");
            Size = size;
            _data = new T[Size * Size];
        }
        /// <summary>
        /// Indexator that give users opportunity to work with definite element by writing its indexes.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public T this[int row, int col]
        {
            get 
            {
                if (row < 0 || col < 0 || row > Size || col > Size) throw new ArgumentOutOfRangeException("Wrong indexes.");
                return _data[row * Size + col];
            }

            set
            {
                if (row < 0 || col < 0 || row > Size || col > Size) throw new ArgumentOutOfRangeException("Wrong indexes.");
                _data[row * Size + col] = value;
            }
        } 
        public delegate void MatrixHandler(int[] indexes, T oldValue);
        public event MatrixHandler? Notify;
    }
}
