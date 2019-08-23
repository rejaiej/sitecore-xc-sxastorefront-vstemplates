namespace Customer.Commerce.XA.Storefront.Extension.Configurations
{
    public class HelixConfiguration
    {
        public string Layer { get; set; }

        public string BusinessObjectiveName { get; set; }
        
        public bool ProvisionTestsFolder { get; set; }

        public bool ProvisionEngineFolder { get; set; }

        public bool ProvisionSerializationFolder { get; set; }
    }
}