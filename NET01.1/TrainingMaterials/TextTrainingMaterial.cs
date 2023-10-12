namespace ConsoleApp1.TrainingMaterials
{
    internal class TextTrainingMaterial : TrainingMaterial
    {
        public string Text { get;}
        private const int maxLength = 10000;
        public TextTrainingMaterial(string text, string? description)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException();
            if (text.Length > maxLength)
            {
                Text = text[..maxLength];
            }
            else
            {
                Text = text;
            }
            Id = Guid.NewGuid();
            Description = description;
        }
    }
}
