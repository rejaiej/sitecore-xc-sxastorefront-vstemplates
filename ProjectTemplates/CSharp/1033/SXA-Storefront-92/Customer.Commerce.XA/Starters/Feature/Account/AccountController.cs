namespace $safeprojectname$.Feature.Account.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;

    using Sitecore.Commerce.XA.Foundation.Common.Attributes;
    using Sitecore.Commerce.XA.Foundation.Common.Context;
    using Sitecore.Commerce.XA.Foundation.Common.Models;
    using Sitecore.Commerce.XA.Feature.Account.Repositories;
    using Sitecore.Commerce.XA.Foundation.Connect.Managers;
    using Sitecore.Diagnostics;

    using Repositories;

    public class $accountControllerName$ : Sitecore.Commerce.XA.Feature.Account.Controllers.AccountController
    {
        public $accountControllerName$(
            ILoginRepository loginRepository,
            IRegistrationRepository registrationRepository,
            IForgotPasswordRepository forgotPasswordRepository,
            IChangePasswordRepository changePasswordRepository,
            $sampleIRepositoryName$ sampleRepository,
            IAccountManager accountManager,
            IStorefrontContext storefrontContext,
            IModelProvider modelProvider,
            IContext sitecoreContext,
            IRegisterUserRepository registerUserRepository,
            ILoginUserRepository loginUserRepository)
          : base(loginRepository, registrationRepository, forgotPasswordRepository, changePasswordRepository, accountManager, storefrontContext, modelProvider, sitecoreContext, registerUserRepository, loginUserRepository)
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