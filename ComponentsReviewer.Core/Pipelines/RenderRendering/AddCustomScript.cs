using Sitecore.Mvc.Pipelines.Response.RenderRendering;

namespace ComponentsReviewer.Pipelines.RenderRendering
{
    public class AddCustomScript:RenderRenderingProcessor
    {
        public override void Process(RenderRenderingArgs args)
        {
            args.Writer.WriteLine("<script>alert('works!!!!!!!!')</script>");
        }
    }
}