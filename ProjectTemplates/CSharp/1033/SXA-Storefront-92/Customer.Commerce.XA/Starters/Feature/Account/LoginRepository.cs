namespace $safeprojectname$.Repositories
{
    using Sitecore.Commerce.XA.Foundation.Common.Context;
    using Sitecore.Commerce.XA.Foundation.Common.Models;

    public class LoginRepository : Sitecore.Commerce.XA.Feature.Account.Repositories.LoginRepository
    {
        public LoginRepository(
            IModelProvider modelProvider,
            IStorefrontContext storefrontContext,
            IContext context)
             : base(modelProvider, storefrontContext, context)
        {
        }
    }
}