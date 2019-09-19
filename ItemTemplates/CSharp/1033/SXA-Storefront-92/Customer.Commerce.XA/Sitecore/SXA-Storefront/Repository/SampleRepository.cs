namespace $rootnamespace$
{
    using Sitecore.Commerce.XA.Foundation.Common.Models;
    using Sitecore.Commerce.XA.Foundation.Common.Repositories;
    using Sitecore.Diagnostics;
    using Sitecore.XA.Foundation.SitecoreExtensions.Interfaces;

    public class $safeitemname$ : BaseCommerceModelRepository, I$safeitemname$
    {
        public $safeitemrootname$(
            IModelProvider modelProvider)
        {
            Assert.ArgumentNotNull(modelProvider, nameof(modelProvider));
            this.ModelProvider = modelProvider;
        }

        public IModelProvider ModelProvider { get; protected set; }

        public virtual object GetAllItems(
            IRendering rendering)
        {
            object model = default(object);
            return model;
        }
    }
}