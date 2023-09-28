using ConsoleApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingMaterials
{
    /// <summary>
    /// Material for training that is represented by some video.
    /// </summary>
    class VideoTrainingMaterial : TrainingMaterial, IVersionable
    {
        public string URIVideo { get; }
        public string URIPicture { get;}
        public string Format { get; }

        private readonly string[] validFormats = { "Unknown", "Avi", "Mp4", "Flv" };
        private byte[] _version;
        public VideoTrainingMaterial(string uriVideo, string uriPicture, string format, string? description, byte[] version)
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
            SetVersion(version);
        }

        /// <summary>
        /// Check if fromat of video is available
        /// </summary>
        /// <param name="format"></param>
        /// <returns>True or false</returns>
        private bool IsValidVideoFormat(string format)
        {
            
            return Array.Exists(validFormats, f => f.Equals(format, StringComparison.OrdinalIgnoreCase));
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
