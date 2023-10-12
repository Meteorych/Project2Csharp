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
    internal class DiagonalMatrix<T> : SquareMatrix<T>
    {
        public DiagonalMatrix(int dimension) : base(dimension) 
        {
            //Перегрузка индексатора
            //Меньше элементов в одномерном массиве
            //Модульные тесты
            for (int i = 0; i < Dimension; i++) 
            {
                Data[(dimension + 1) * i] = default!;
            }
        }
    }
}
