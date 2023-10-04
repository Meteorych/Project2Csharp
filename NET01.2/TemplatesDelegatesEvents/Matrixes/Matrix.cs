using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplatesDelegatesEvents.Matrixes
{
    /// <summary>
    /// Класс-родитель для представления матрицы
    /// </summary>
    abstract class Matrix<T>
    {
        public delegate void MatrixHandler(int[] indexes, T oldValue);
        public event MatrixHandler chan;
    }
}
