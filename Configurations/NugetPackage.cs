namespace Customer.Commerce.XA.Storefront.Extension.Configurations
{
    public class NuGetPackage
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public bool IgnoreDependencies { get; set; } = true;
    }
}