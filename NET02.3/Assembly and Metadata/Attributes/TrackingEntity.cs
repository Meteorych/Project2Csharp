namespace Assembly_and_Metadata.Attributes;
/// <summary>
/// Attribute class that marks object for tracking.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TrackingEntity : Attribute;