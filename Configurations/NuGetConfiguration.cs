namespace Customer.Commerce.XA.Storefront.Extension.Configurations
{
    using System.Collections.Generic;
    using System.Linq;

    public class NuGetConfiguration
    {
        public IList<NuGetPackage> Packages { get; set; } = new List<NuGetPackage>();
    }
}