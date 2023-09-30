using ConsoleApp1.TrainingMaterials;
using ConsoleApp1.Lessons;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            Lesson testlesson1 = new(materials: new List<TrainingMaterial>() { new HTMLTrainingMaterial("fsafaf", "Html") }, version: new byte[] { 0, 1, 1, 4, 6 });
            Lesson testlesson2 = (Lesson)testlesson1.Clone();
            Console.WriteLine(testlesson1.LessonMaterials[0].Id);
            Console.WriteLine(testlesson2.LessonMaterials[0].Id);
        }
    }
}