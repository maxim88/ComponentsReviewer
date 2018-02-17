using System;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;

namespace ComponentsReviewer.Pipelines.Initialize
{
    public class InitializeRotes
    {
        public void Process(PipelineArgs args)
        {
            try
            {

                MvcHandler.DisableMvcResponseHeader = true;
                RouteTable.Routes.MapRoute("ComponentsReviewerApi", "api/{controller}/{action}", new
                {
                    action = "Index"
                });
            }
            catch (Exception e)
            {
                Log.Error("InistializeRoting failed: " + e.Message, e);
            }
        }
    }
}