using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TrainingMaterials
{
    internal class TextTraining : TrainingMaterial
    {
        public string Text { get;}
        private readonly int _maxLength = 10000;
        public TextTraining(string text, string? description)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException();
            if (text.Length > _maxLength)
            {
                Text = text[.._maxLength];
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
