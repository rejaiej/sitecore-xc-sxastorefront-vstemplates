namespace $safeprojectname$.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;

    using Sitecore.Commerce.XA.Foundation.Common.Attributes;
    using Sitecore.Commerce.XA.Foundation.Common.Context;
    using Sitecore.Commerce.XA.Foundation.Common.Models;
    using Sitecore.Commerce.XA.Feature.Cart.Repositories;
    using Sitecore.Diagnostics;

    using Repositories;

    public class $cartControllerName$ : Sitecore.Commerce.XA.Feature.Cart.Controllers.CartController
    {
        public $cartControllerName$(
            IStorefrontContext storefrontContext,
            IModelProvider modelProvider,
            IAddToCartRepository addToCartRepository,
            IMinicartRepository minicartRepository,
            IPromotionCodesRepository promotionCodesRepository,
            IShoppingCartLinesRepository shoppingCartLinesRepository,
            IShoppingCartTotalRepository shoppingCartTotalRepository,
            $sampleIRepositoryName$ sampleRepository,
            IContext sitecoreContext)
          : base(storefrontContext, modelProvider, addToCartRepository, minicartRepository, promotionCodesRepository, shoppingCartLinesRepository, shoppingCartTotalRepository, sitecoreContext)
        {
            Assert.ArgumentNotNull(sampleRepository, nameof(sampleRepository));
            this.SampleRepository = sampleRepository;
        }

        public $sampleIRepositoryName$ SampleRepository { get; protected set; }

        [StorefrontSessionState(SessionStateBehavior.ReadOnly)]
        public ActionResult GetAllSampleItems()
        {
            return this.View(this.Rendering.RenderingViewPath, this.SampleRepository.GetAllItems(this.Rendering));
        }
    }
}