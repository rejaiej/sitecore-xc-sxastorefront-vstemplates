namespace $safeprojectname$.Pipelines.Initialize
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Sitecore.Pipelines;

    public class RegisterRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute(
                "AccountOverride",
                "{virtualFolder}/api/cxa/catalog/{action}",
                new { controller = "Catalog"},
                new[] { "$safeprojectname$.Controllers" }
            );
        }
    }
}