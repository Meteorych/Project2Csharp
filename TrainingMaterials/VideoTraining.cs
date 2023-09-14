using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingMaterials
{
    class VideoTraining : TrainingMaterial
    {
        public string URIVideo { get; }
        public string URIPicture { get;}
        public string Format { get; }
        public VideoTraining(string uriVideo, string uriPicture, string format, string? description)
        {
            if (string.IsNullOrEmpty(uriVideo) || string.IsNullOrEmpty(uriPicture))
            {
                throw new ArgumentException("URI of picture and URI of video can't be empty or null");
            }
            if (IsValidVideoFormat(format) is false)
            {
                throw new ArgumentException("Wrong type of format!");
            }
            URIVideo = uriVideo;
            URIPicture = uriPicture;
            Format = format;
            Id = Guid.NewGuid();
            Description = description;
        }

        private bool IsValidVideoFormat(string format)
        {
            // Проверка на допустимые форматы видео
            string[] validFormats = { "Unknown", "Avi", "Mp4", "Flv" };
            return Array.Exists(validFormats, f => f.Equals(format, StringComparison.OrdinalIgnoreCase));
        }
    }
}
