using Assembly_and_Metadata.Attributes;

namespace Assembly_and_Metadata.TestEntities
{
    /// <summary>
    /// Test entity for tracking.
    /// </summary>
    [TrackingEntity]
    public class Person
    {
        public int Id { get; set; } = 1;

        [TrackingProperty(PropertyName = "Name of person")]
        public string Name { get; set; } = "Mike";

        public string Description { get; set; } = string.Empty;

        [TrackingProperty(PropertyName = nameof(ParentName))]
        public string ParentName { get; private set; } = "John";
    }
}
