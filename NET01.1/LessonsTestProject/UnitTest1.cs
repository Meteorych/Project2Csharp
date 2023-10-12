using ConsoleApp1.Lessons;
using ConsoleApp1.TrainingMaterials;

namespace LessonsTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Lesson_CorrectCloningTest()
        {
            Lesson testLesson1 = new(materials: new List<TrainingMaterial>() { new HtmlTrainingMaterial("TestURI", "Html") }, version: new byte[] { 0, 1, 1, 4, 6 });
            var testLesson2 = (Lesson)testLesson1.Clone();
            CollectionAssert.AreEqual(testLesson1.GetVersion(), testLesson2.GetVersion());
            Assert.AreNotEqual(testLesson2, testLesson1);
            CollectionAssert.AreNotEqual(testLesson2.LessonMaterials, testLesson1.LessonMaterials);
        }
    }
}