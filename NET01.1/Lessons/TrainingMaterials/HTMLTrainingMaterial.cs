namespace ConsoleApp1.TrainingMaterials
{
    public class HtmlTrainingMaterial : TrainingMaterial
    {
        public string UriContent { get; }
        public string LinkType { get; }
        public HtmlTrainingMaterial(string uri, string linkType, string? description = null)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (!IsValidLinkType(linkType))
            {
                throw new ArgumentException("Wrong link type!");

            }
            UriContent = uri;
            LinkType = linkType;
            Id = Guid.NewGuid();
            SetGetDescription = description;
        }
        public static bool IsValidLinkType(string linkType) 
        {
            string[] validTypes = { "Unknown", "Html", "Image", "Audio", "Video" };
            return Array.Exists(validTypes, f => f.Equals(linkType, StringComparison.OrdinalIgnoreCase));
        }
    }
}
