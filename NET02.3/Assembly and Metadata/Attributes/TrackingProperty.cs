namespace Assembly_and_Metadata.Attributes;
/// <summary>
/// Attribute class to mark 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class TrackingProperty : Attribute
{
    public string PropertyName { get; set; }
};