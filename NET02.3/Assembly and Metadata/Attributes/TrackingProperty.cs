namespace Assembly_and_Metadata.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class TrackingProperty : Attribute
    {
        public string PropertyName { get; set; }
    };
}
