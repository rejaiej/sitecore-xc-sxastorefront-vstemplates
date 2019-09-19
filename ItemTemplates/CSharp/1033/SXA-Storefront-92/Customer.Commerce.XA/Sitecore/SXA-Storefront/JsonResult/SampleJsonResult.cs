namespace $rootnamespace$
{
    using System.Collections.Generic;
    using System.Linq;

    using Sitecore.Commerce.XA.Foundation.Common.Context;
    using Sitecore.Commerce.XA.Foundation.Common.Models.JsonResults;
    using Sitecore.Diagnostics;
    using Sitecore.XA.Foundation.SitecoreExtensions.Interfaces;

    public class $safeitemname$ : BaseJsonResult
    {
        public $safeitemname$(IStorefrontContext storefrontContext, IContext context)
            : base(context, storefrontContext)
        {
        }

        public IList<string> Items { get; set; }

        public virtual void Initialize(IRendering rendering)
        {
            Assert.IsNotNull(rendering, $"$safeitemname$.Initialize: Parameter {nameof(rendering)} cannot be null.");

            this.Items = new List<string>()
            {
                "Item1",
                "Item2",
                "Item3"
            };
        }
    }
}