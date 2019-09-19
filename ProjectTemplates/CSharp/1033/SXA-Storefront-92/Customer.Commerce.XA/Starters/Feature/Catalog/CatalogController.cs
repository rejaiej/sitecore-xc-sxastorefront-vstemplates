namespace $safeprojectname$.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;

    using Sitecore.Commerce.XA.Foundation.Common.Attributes;
    using Sitecore.Commerce.XA.Foundation.Common.Context;
    using Sitecore.Commerce.XA.Foundation.Common.Models;
    using Sitecore.Commerce.XA.Feature.Catalog.Repositories;
    using Sitecore.Diagnostics;

    using Repositories;

    public class $catalogControllerName$ : Sitecore.Commerce.XA.Feature.Catalog.Controllers.CatalogController
    {
        public $catalogControllerName$(
            IModelProvider modelProvider,
            IProductListHeaderRepository productListHeaderRepository,
            IProductListRepository productListRepository,
            IPromotedProductsRepository promotedProductsRepository,
            IProductInformationRepository productInformationRepository,
            IProductImagesRepository productImagesRepository,
            IProductInventoryRepository productInventoryRepository,
            IProductPriceRepository productPriceRepository,
            IProductVariantsRepository productVariantsRepository,
            IProductBundleRepository productBundleRepository,
            IProductListPagerRepository productListPagerRepository,
            IProductFacetsRepository productFacetsRepository,
            IProductListSortingRepository productListSortingRepository,
            IProductListPageInfoRepository productListPageInfoRepository,
            IProductListItemsPerPageRepository productListItemsPerPageRepository,
            ICatalogItemContainerRepository catalogItemContainerRepository,
            IVisitedCategoryPageRepository visitedCategoryPageRepository,
            IVisitedProductDetailsPageRepository visitedProductDetailsPageRepository,
            ISearchInitiatedRepository searchInitiatedRepository,
            ISampleRepository sampleRepository,
            IStorefrontContext storefrontContext,
            ISiteContext siteContext,
            IContext sitecoreContext)
          : base(
                modelProvider, 
                productListHeaderRepository, 
                productListRepository, 
                promotedProductsRepository, 
                productInformationRepository, 
                productImagesRepository, 
                productInventoryRepository, 
                productPriceRepository, 
                productVariantsRepository,
                productBundleRepository, 
                productListPagerRepository, 
                productFacetsRepository, 
                productListSortingRepository, 
                productListPageInfoRepository, 
                productListItemsPerPageRepository, 
                catalogItemContainerRepository, 
                visitedCategoryPageRepository, 
                visitedProductDetailsPageRepository, 
                searchInitiatedRepository,
                storefrontContext, 
                siteContext, 
                sitecoreContext)
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