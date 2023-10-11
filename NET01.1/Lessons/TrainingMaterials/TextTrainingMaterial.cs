using ConsoleApp1.Interfaces;

namespace ConsoleApp1.TrainingMaterials
{
    public class TextTrainingMaterial : TrainingMaterial
    {
        public string Text { get;}
        private const int MaxLength = 10000;
        public TextTrainingMaterial(string text, string? setGetDescription)
        {
            if (string.IsNullOrEmpty(text) || text.Length > MaxLength) throw new ArgumentNullException(nameof(text));
            Text = text;
            Id = Guid.NewGuid();
            ((BaseEntity)this).SetGetDescription = setGetDescription;
        }
    }
}
