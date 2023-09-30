using ConsoleApp1.Lessons;
using ConsoleApp1.TrainingMaterials;
using System.Security.Cryptography.X509Certificates;

namespace LessonTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Lesson_CorrectCloningTest()
        {
            Lesson testlesson1 = new(materials: new List<TrainingMaterial>() { new HTMLTrainingMaterial("fsafaf", "Html") }, version: new byte[] { 0, 1, 1, 4, 6 });
            Lesson testlesson2 = (Lesson)testlesson1.Clone();
            CollectionAssert.AreEqual(testlesson1.GetVersion(), testlesson2.GetVersion());
            Assert.AreNotEqual(testlesson2, testlesson1);
            CollectionAssert.AreNotEqual(testlesson2.LessonMaterials, testlesson1.LessonMaterials);
        }
    }
}