using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplatesDelegatesEvents.Matrices
{
    /// <summary>
    /// Derived class for creating model of Diagonal Matrix on the base of Square Matrix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class DiagonalMatrix<T> : SquareMatrix<T>
    {
        public DiagonalMatrix(int size) : base(size) 
        {
            for (int i = 0; i < Size; i++) 
            {
                Data[(size + 1) * i] = default;
            }
        }
    }
}
