using ConsoleApp1.Interfaces;

namespace ConsoleApp1.TrainingMaterials
{
    public abstract class TrainingMaterial : BaseEntity, ICloneable
    {
        public new string? ToString => Description;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
