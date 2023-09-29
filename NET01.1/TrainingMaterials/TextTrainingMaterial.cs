namespace ConsoleApp1.TrainingMaterials
{
    internal class TextTrainingMaterial : TrainingMaterial
    {
        public string Text { get;}
        private const int maxLength = 10000;
        public TextTrainingMaterial(string text, string? description)
        {
            if (string.IsNullOrEmpty(text) || text.Length > maxLength) throw new ArgumentNullException("Plese input text of right length!");
            Text = text;
            Id = Guid.NewGuid();
            Description = description;
        }
    }
}
