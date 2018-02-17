using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Sitecore.Pipelines.ConvertToDesignTimeHtml;

namespace ComponentsReviewer.Pipelines.PrepareHtml
{
    public class UpdateRenderingHtml
    { 
            public void Process(ConvertToDesignTimeHtmlArgs args)
            { 
                Assert.ArgumentNotNull(args, "args");
                args.Html = XHtml.PrepareHtml(args.Html);
            }
        }
    }
 