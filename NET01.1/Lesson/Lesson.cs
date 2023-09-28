using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interfaces;
using ConsoleApp1.TrainingMaterials;

namespace ConsoleApp1.Lesson
{
    public enum LessonType
    {
        TextLesson,
        VideoLesson
    }
    class TextLesson : BaseEntity, IVersionable, ICloneable
    {
        public Guid Id { get; set; }
        private List<TrainingMaterial> _lessonMaterials;
        private byte[] _version;
        public LessonType LessonType { get; }
        public byte[] Version { get; private set; }
      
        public TextLesson(string? description, List<TrainingMaterial> materials, byte[] version)
        {
            Description = description;
            Id = Guid.NewGuid();
            _lessonMaterials = materials;
            LessonType = LessonType.TextLesson;
            if (_lessonMaterials.Any(material => material is VideoTrainingMaterial))
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
            var lesson = (TextLesson)MemberwiseClone();
            lesson._lessonMaterials = new List<TrainingMaterial>();
            foreach (var material in _lessonMaterials)
            {
                if (material is VideoTrainingMaterial videoMaterial)
                {
                    // Deep clone VideoTraining
                    var clonedVideoMaterial = new VideoTraining(videoMaterial.URIVideo, videoMaterial.URIPicture, videoMaterial.Format, videoMaterial.Description, videoMaterial.Version);
                    lesson._lessonMaterials.Add(clonedVideoMaterial);
                }
                else if (material is TextTrainingMaterial textMaterial)
                {
                    // Deep clone TextTraining
                    var clonedTextMaterial = new TextTraining(textMaterial.Text, textMaterial.Description);
                    lesson._lessonMaterials.Add(clonedTextMaterial);
                }
            }
            return lesson;
        }
        public bool Equals(string otherId)
        {
            return Id.Equals(otherId);
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
    class VideoLesson : TextLesson
    {
        public VideoLesson(string? description, List<TrainingMaterial> materials, byte[] version) : base(description, materials, version)
        {
        }
    }
}


