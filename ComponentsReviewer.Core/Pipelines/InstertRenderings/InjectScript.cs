using System.Web;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.InsertRenderings;

namespace ComponentsReviewer.Pipelines.InstertRenderings
{
    public class InjectScript: InsertRenderingsProcessor
    {
        public override void Process(InsertRenderingsArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (args.HasRenderings)
            {
                string html = "";
                var design = args.Renderings[0].RenderingItem
                    .GetDesignTimeHtml(HttpContext.Current.Request.QueryString, ref html);
            }
           
           // args.ContextItem = Context.Item;
        }
    } 
}