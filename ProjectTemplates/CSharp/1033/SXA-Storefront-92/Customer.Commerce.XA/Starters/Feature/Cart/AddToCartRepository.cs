namespace $safeprojectname$.Repositories
{
    using Sitecore.Commerce.XA.Foundation.Common.Context;
    using Sitecore.Commerce.XA.Foundation.Connect.Managers;
    using Sitecore.Commerce.XA.Foundation.Common.Models;

public class AddToCartRepository : Sitecore.Commerce.XA.Feature.Cart.Repositories.AddToCartRepository
{
        public AddToCartRepository(
            IModelProvider modelProvider,
            ICartManager cartManager,
            ISiteContext siteContext)
             : base(modelProvider, cartManager, siteContext)
        {
        }
    }
}