using ConsoleApp1.TrainingMaterials;
using ConsoleApp1.ExtensionMethod;
namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            TrainingMaterial trainingMaterial = new TextTraining("ssfafafasf", null);
            Console.WriteLine(trainingMaterial.Id);
            trainingMaterial.GenerateNewId();
            Console.WriteLine(trainingMaterial.Id);
        }
    }
}