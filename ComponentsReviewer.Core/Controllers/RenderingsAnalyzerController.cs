using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ComponentsReviewer.Models;
using ComponentsReviewer.Repository;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.StringExtensions;

namespace ComponentsReviewer.Controllers 
{
    public class ComponentsReviewerController : Controller
    {
        public JsonResult GetAllRenderings() 
        {
            var rendringItems = RenderingsGetter.GetAllRenderings();
            var activeRenderings = StateProvider.GetActiveRenderigIds();

            var res = new Results
            {
                Epics = new List<Location>(),
                Renderings = new List<RenderingProperties>()
            }; 

            foreach (var item in rendringItems)
            {
                var rendering = RenderingPropertiesHelper.GetRenderingsProperties(item);
                
                if (rendering != null)
                {
                    rendering.IsActive = activeRenderings.Any(r => r.Guid == rendering.Id);
                    res.Renderings.Add(rendering);
                }
            }

            res.Epics.AddRange(LocationsHelper.GetEpics());
             
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllRefferences(Guid renderingId)
        {
            var references = LinksProvider.GetRefferencedItems(renderingId);
            return Json(references, JsonRequestBehavior.AllowGet);
        } 
           
        [HttpPost]
        public JsonResult ActivateRenderings(List<Guid> renderingIds)
        {
            var res = "success"; 
            try
            {
                if (renderingIds == null)
                {
                    res = "renderingId has wrong format";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                
                StateProvider.ActivateRendering(renderingIds.Select(r=>new ID(r)).ToList());
            }
            catch (Exception e)
            {
                Log.Error("ActivateRenderings failed: " + e.Message, e, this);
                res = "Server Error";
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeactivateRenderings(List<Guid> renderingIds)
        {
            var res = "success";

            try
            {
                if (renderingIds == null)
                {
                    res = "renderingId has wrong format";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }

                StateProvider.DeactivateRendering(renderingIds.Select(r=>new ID(r)).ToList());
            }
            catch (Exception e)
            {
                Log.Error("DeactivateRenderings failed: " + e.Message, e, this);
                res = "Server Error";
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveRenderingsProperties(RenderingProperties renderingProperties)
        {
            var res = "success";

            try
            {
                RenderingPropertiesHelper.SaveRenderingProperties(renderingProperties); 
            }
            catch (Exception e)
            {
                Log.Error("DeactivateRenderings failed: " + e.Message, e, this);
                res = "Server Error";
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Save(Result result)
        {
            var folderPath = Request.PhysicalApplicationPath + "Renderings Review Helper/";
            var filePath = "{0}{1}.png".FormatWith(folderPath, Guid.NewGuid());

            bool folderExists = Directory.Exists(folderPath);
            if (!folderExists)
                Directory.CreateDirectory(Server.MapPath(folderPath));

            if (result.IsStart)
            {
                DirectoryInfo di = new DirectoryInfo(folderPath);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
            
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {

                        using (BinaryWriter bw = new BinaryWriter(fs))

                        {

                            byte[] data = Convert.FromBase64String(result.imgBase64.Replace("data:image/png;base64,", ""));

                            bw.Write(data);

                            bw.Close();
                        }

                    }
                }
                catch (Exception exception)
                {
                    throw new Exception("Error in base64Encode" + exception.Message);
                }

            if (result.IsEnd)
            {

            }
        }
         
        public class Result
        {
            public string imgBase64 { get; set; }
            public bool IsStart { get; set; }
            public bool IsEnd { get; set; }
        }
    }
}