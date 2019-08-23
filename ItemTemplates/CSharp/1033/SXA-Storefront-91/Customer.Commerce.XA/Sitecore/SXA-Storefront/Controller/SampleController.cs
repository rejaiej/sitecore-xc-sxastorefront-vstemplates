namespace $rootnamespace$
{
    using System.Web.Mvc;
    using System.Web.SessionState;

    using Sitecore.Commerce.XA.Foundation.Common.Attributes;
    using Sitecore.Commerce.XA.Foundation.Common.Context;
    using Sitecore.Commerce.XA.Foundation.Common.Controllers;
    using Sitecore.Commerce.XA.Foundation.Common.Models;
    using Sitecore.Diagnostics;

    public class $safeitemname$ : BaseCommerceStandardController
    {
        public $safeitemname$(
            IStorefrontContext storefrontContext,
            IContext sitecoreContext)
          : base(storefrontContext, sitecoreContext)
        {
        }

        [StorefrontSessionState(SessionStateBehavior.ReadOnly)]
        public ActionResult GetAllItems()
        {
            return this.View(this.Rendering.RenderingViewPath, default(object));
        }
    }
}