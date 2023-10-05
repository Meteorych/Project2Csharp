using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplatesDelegatesEvents.Matrixes
{
    internal class DiagonalMatrix<T> : Matrix<T>
    {
        public DiagonalMatrix(int size) : base(size) 
        {
            for (int i = 0; i < Size; i++) 
            {
                _data[(size + 1) * i] = default(T);
            }
        }
    }
}
