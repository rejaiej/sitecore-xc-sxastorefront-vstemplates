namespace Customer.Commerce.XA.Storefront.Extension.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    public static class XDocumentExtensions
    {
        public static XElement GetXElementWithNoNamespace(this XElement element, string name)
        {
            if (element == null) return default;

            return QueryXElementsByLocalName(element.Elements(), name).FirstOrDefault();
        }

        public static IEnumerable<XElement> GetXElementsWithNoNamespace(this XElement element, string name)
        {
            if (element == null) return default;

            return QueryXElementsByLocalName(element.Elements(), name);
        }

        private static IEnumerable<XElement> QueryXElementsByLocalName(IEnumerable<XElement> elements, string name)
        {
            return elements.Where(element => string.Equals(element.Name.LocalName, name, System.StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
