using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp1.TrainingMaterials
{
    public class HTMLTrainingMaterial : TrainingMaterial
    {
        public string URIContent { get; }
        public string LinkType { get; }
        public HTMLTrainingMaterial(string URI, string linkType, string? description = null)
        {
            if (string.IsNullOrEmpty(URI))
            {
                throw new ArgumentNullException("URI can't be empty!");
            }
            if (!IsValidLinkType(linkType))
            {
                throw new ArgumentException("Wrong link type!");

            }
            URIContent = URI;
            LinkType = linkType;
            Id = Guid.NewGuid();
            Description = description;
        }
        public bool IsValidLinkType(string linkType) 
        {
            string[] validTypes = { "Unknown", "Html", "Image", "Audio", "Video" };
            return Array.Exists(validTypes, f => f.Equals(linkType, StringComparison.OrdinalIgnoreCase));
        }
    }
}
