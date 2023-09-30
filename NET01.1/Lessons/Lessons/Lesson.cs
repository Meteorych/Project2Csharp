﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private List<TrainingMaterial> _lessonMaterials;
        private byte[] _version;
        public LessonType LessonType { get; }
        public List<TrainingMaterial> LessonMaterials { get { return _lessonMaterials; } }
      
        public Lesson(List<TrainingMaterial> materials, byte[] version, string? description = null)
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
            var lesson = (Lesson)MemberwiseClone();
            lesson._lessonMaterials = new List<TrainingMaterial>();
            foreach (var material in _lessonMaterials)
            {
                lesson._lessonMaterials.Add((TrainingMaterial)material.Clone());
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
}

