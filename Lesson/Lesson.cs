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
    class TextLesson : ITraining, IVersionable, ICloneable
    {
        public Guid Id { get; }
        private string? _description;
        private List<TrainingMaterial> _lessonMaterials;
        public LessonType LessonType { get; }
        public byte[] Version { get; private set; }
        public string? Description
        {
            get { return _description; }
            set
            {
                if (value.Length > 256)
                {
                    _description = value[..256];
                }
                else
                {
                    _description = value;
                }
            }
        }
        public TextLesson(string? description, List<TrainingMaterial> materials, byte[] version)
        {
            Description = description;
            Id = Guid.NewGuid();
            _lessonMaterials = materials;
            if (_lessonMaterials.Any(material => material is VideoTraining))
            {
                LessonType = LessonType.VideoLesson;
            }
            else
            {
                LessonType = LessonType.TextLesson;
            }
            SetVersion(version);
        }
        public void SetVersion(byte[] version) { Version = version; }

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
                if (material is VideoTraining videoMaterial)
                {
                    // Deep clone VideoTraining
                    var clonedVideoMaterial = new VideoTraining(videoMaterial.URIVideo, videoMaterial.URIPicture, videoMaterial.Format, videoMaterial.Description, videoMaterial.Version);
                    lesson._lessonMaterials.Add(clonedVideoMaterial);
                }
                else if (material is TextTraining textMaterial)
                {
                    // Deep clone TextTraining
                    var clonedTextMaterial = new TextTraining(textMaterial.Text, textMaterial.Description);
                    lesson._lessonMaterials.Add(clonedTextMaterial);
                }
            }
            return lesson;
        }

    }
    class VideoLesson : TextLesson
    {
        public VideoLesson(string? description, List<TrainingMaterial> materials, byte[] version) : base(description, materials, version)
        {
        }
    }
}


