using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interfaces;
using ConsoleApp1.TrainingMaterials;
//Сделать Тип
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
        private string? _description;
        private List<TrainingMaterial> _lessonMaterials;
        public LessonType LessonType { get; }
        public byte[] Version { get; private set; }
        public string? Description
        {
            get { return _description; }
            set
            {
                if (value == null)
                {
                    _description = null;
                }
                else if (value.Length > 256)
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
            LessonType = LessonType.TextLesson;
            if (_lessonMaterials.Any(material => material is VideoTrainingMaterial))
            {
                LessonType = LessonType.VideoLesson;
            }
            SetVersion(version);
        }
        public void SetVersion(byte[] version) { Version = version; }

        /// <summary>
        /// Deep cloning of object lesson!
        /// </summary>
        /// <returns></returns>
        //ICloneable в training material
        public object Clone()
        {
            var lesson = (TextLesson)MemberwiseClone();
            lesson._lessonMaterials = new List<TrainingMaterial>();
            foreach (var material in _lessonMaterials)
            {
                if (material is VideoTrainingMaterial videoMaterial)
                {
                    // Deep clone VideoTraining
                    var clonedVideoMaterial = new VideoTrainingMaterial(videoMaterial.URIVideo, videoMaterial.URIPicture, videoMaterial.Format, videoMaterial.Description, videoMaterial.Version);
                    lesson._lessonMaterials.Add(clonedVideoMaterial);
                }
                else if (material is TextTrainingMaterial textMaterial)
                {
                    // Deep clone TextTraining
                    var clonedTextMaterial = new TextTrainingMaterial(textMaterial.Text, textMaterial.Description);
                    lesson._lessonMaterials.Add(clonedTextMaterial);
                }
            }
            return lesson;
        }
        public bool Equals(string otherId)
        {
            return Id.Equals(otherId);
        }
    }
    
}


