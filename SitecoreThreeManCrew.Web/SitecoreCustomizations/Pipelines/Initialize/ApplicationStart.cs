using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;
using SitecoreThreeManCrew.Web.App_Start;

namespace SitecoreThreeManCrew.Web.SitecoreCustomizations.Pipelines.Initialize
{
    public class ApplicationStart
    {
        public void Process(PipelineArgs args)
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}