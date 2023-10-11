using ConsoleApp1.Interfaces;

namespace ConsoleApp1.TrainingMaterials
{
    public abstract class TrainingMaterial : BaseEntity, ICloneable
    {
        public new string? ToString => _description;

        public bool Equals(string otherId)
        {
            return Id.ToString().Equals(otherId);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
