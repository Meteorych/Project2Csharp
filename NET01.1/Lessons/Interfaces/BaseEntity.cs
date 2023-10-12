namespace ConsoleApp1.Interfaces
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set;}
        protected string? Description;
        public bool Equals(string otherId)
        {
            return Id.ToString().Equals(otherId);
        }
        public string? SetGetDescription
        {
            get => Description;
            set => Description = value;
        }
    }
}
