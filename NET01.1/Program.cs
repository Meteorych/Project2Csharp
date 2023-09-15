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
            ObjectExtension.GenerateNewId(trainingMaterial);
            Console.WriteLine(trainingMaterial.Id);
        }
    }
}