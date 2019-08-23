namespace $safeprojectname$.Repositories
{
    using Sitecore.Commerce.XA.Foundation.Catalog.Managers;
    using Sitecore.Commerce.XA.Foundation.Common.Models;
    using Sitecore.Commerce.XA.Foundation.Common.Context;
    using Sitecore.Commerce.XA.Foundation.Common.Search;
    using Sitecore.Commerce.XA.Foundation.Connect.Managers;

    public class PromotedProductsRepository : Sitecore.Commerce.XA.Feature.Catalog.Repositories.PromotedProductsRepository
    {
        public PromotedProductsRepository(
            IModelProvider modelProvider,
            IStorefrontContext storefrontContext,
            ISiteContext siteContext,
            ISearchInformation searchInformation,
            ISearchManager searchManager,
            IInventoryManager inventoryManager,
            ICatalogManager catalogManager,
            ICatalogUrlManager catalogUrlManager,
            IContext context)
                : base(modelProvider, storefrontContext, siteContext, searchInformation, searchManager, inventoryManager, catalogManager, catalogUrlManager, context)
        {
        }
    }
}