using TemplatesDelegatesEvents.Matrixes;

namespace TemplatesDelegatesEvents
{
    internal class Program
    {
        static void Main()
        {
            SquareMatrix<int> matrix = new(5);
            //Anonimous method for handling events
            matrix.ElementChanged += delegate (object? sender, EventMatrixElementChangedArgs<int> e)
            {
                Console.WriteLine($"Element at ({e.Row}, {e.Col}) changed from {e.OldValue} to {e.NewValue}");
            };
            //Calling of regular method
            matrix.ElementChanged += MatrixElementChangedHandler;
            //Calling of lambda-function
            matrix.ElementChanged += (sender, e) => Console.WriteLine($"Element at ({e.Row}, {e.Col}) changed from {e.OldValue} to {e.NewValue}");
            matrix[1, 1] = 1;
            matrix[2, 2] = 2;
        }
        /// <summary>
        /// Regular method for handling events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void MatrixElementChangedHandler(object? sender, EventMatrixElementChangedArgs<int> e)
        {
            Console.WriteLine($"Element at ({e.Row}, {e.Col}) changed from {e.OldValue} to {e.NewValue}");
        }
    }
}