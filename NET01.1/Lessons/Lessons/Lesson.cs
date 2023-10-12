using ConsoleApp1.Interfaces;
using ConsoleApp1.TrainingMaterials;

namespace ConsoleApp1.Lessons
{
    public enum LessonType
    {
        TextLesson,
        VideoLesson
    }
    public class Lesson : BaseEntity, IVersionable, ICloneable
    {
        private byte[] _version = null!;
        public LessonType LessonType { get; }
        public List<TrainingMaterial> LessonMaterials { get; private set; }

        public Lesson(List<TrainingMaterial> materials, byte[] version, string? description = null)
        {
            Description = description;
            Id = Guid.NewGuid();
            LessonMaterials = materials;
            LessonType = LessonType.TextLesson;
            if (LessonMaterials.Any(material => material is VideoTrainingMaterial))
            {
                LessonType = LessonType.VideoLesson;
            }
            SetVersion(version);
        }

        /// <summary>
        /// Deep cloning of object lesson!
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var lesson = (Lesson)MemberwiseClone();
            lesson.LessonMaterials = new List<TrainingMaterial>();
            foreach (var material in LessonMaterials)
            {
                lesson.LessonMaterials.Add((TrainingMaterial)material.Clone());
            }
            return lesson;
        }
        
        public void SetVersion(byte[] version)
        {
            _version = version;
        }

        public byte[] GetVersion()
        {
            return _version;
        }
    }
}


