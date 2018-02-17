using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace ComponentsReviewer.Repository
{
    public class RenderingsGetter
    {
        public static List<ID> templates = new IdList
        {
            TemplateIds.Sublayout,
            TemplateIds.ControllerRendering,
            TemplateIds.XmlRendering,
            TemplateIds.ViewRendering
        };


        public static List<Item> GetAllRenderings()
        {
            try
            {
                var rootItems = LocationsHelper.GetRootItems();
                var list = new List<Item>();
                foreach (var rootItem in rootItems)
                {
                    list.AddRange(rootItem.Axes.GetDescendants().Where(d => templates.Any(t => d.TemplateID == t)).ToList());
                } 

                return list.OrderBy(d=>d.Name).ThenBy(d=>d.Paths.FullPath).ToList(); 
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e, typeof(RenderingsGetter));
                return new List<Item>();
            }
        }
        public static Item GetRendering(Guid id)
        {
            try
            {
                var db = Database.GetDatabase(Settings.DatabaseName);
                return db.GetItem(new ID(id));
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e, typeof(RenderingsGetter));
                return null;
            }
        }  
    }
}