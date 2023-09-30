using ConsoleApp1.Interfaces;

namespace ConsoleApp1.TrainingMaterials
{
    public abstract class TrainingMaterial : BaseEntity, ICloneable
    {
        new public string? ToString { get { return _description; }}
        public bool Equals(string otherId)
        {
            return Id.Equals(otherId);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
