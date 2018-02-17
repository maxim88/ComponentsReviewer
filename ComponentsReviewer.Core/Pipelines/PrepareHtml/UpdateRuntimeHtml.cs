using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Sitecore.Pipelines.ConvertToRuntimeHtml;

namespace ComponentsReviewer.Pipelines.PrepareHtml
{
    public class UpdateRuntimeHtml
    {
        public void Process(ConvertToRuntimeHtmlArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            args.Html = XHtml.PrepareHtml(args.Html);
        }
    }
}