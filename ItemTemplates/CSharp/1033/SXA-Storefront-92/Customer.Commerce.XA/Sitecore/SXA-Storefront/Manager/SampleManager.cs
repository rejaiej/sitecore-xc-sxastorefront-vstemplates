namespace $rootnamespace$
{
    using Sitecore.Commerce.XA.Foundation.Common;
    using Sitecore.Commerce.XA.Foundation.Common.Context;
    using Sitecore.Commerce.XA.Foundation.Common.Models;
    using Sitecore.Commerce.XA.Foundation.Common.Utils;
    using Sitecore.Commerce.XA.Foundation.Connect.ExtensionMethods;
    using Sitecore.Commerce.XA.Foundation.Connect.Providers;
    using Sitecore.Diagnostics;
    using Sitecore.XA.Foundation.SitecoreExtensions.Interfaces;

    public class $safeitemname$ : I$safeitemname$
    {
        public $safeitemname$(
            IConnectServiceProvider connectServiceProvider,
            IStorefrontContext storefrontContext,
            IModelProvider modelProvider)
        {
            Assert.ArgumentNotNull(modelProvider, nameof(modelProvider));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));
            this.ModelProvider = modelProvider;
            this.StorefrontContext = storefrontContext;
            this.ConnectServiceProvider = connectServiceProvider;
        }

        public IModelProvider ModelProvider { get; protected set; }

        public IStorefrontContext StorefrontContext { get; protected set; }

        public IConnectServiceProvider ConnectServiceProvider { get; protected set; }

        public virtual object GetAllItems()
        {
            object model = default(object);
            return model;
        }
    }
}