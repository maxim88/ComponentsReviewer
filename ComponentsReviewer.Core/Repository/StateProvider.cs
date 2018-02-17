using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace ComponentsReviewer.Repository
{
    public class StateProvider
    {
        public static void SaveStatus(List<ID> renderingIds)
        {
            var item = GetStateItem();

            using (new SecurityDisabler())
            {
                item.Editing.BeginEdit();
                item[Fields.StateItem.ActiveRenderings] = string.Join("|", renderingIds);
                item.Editing.EndEdit();
            }
        }

        public static void ActivateRendering(List<ID> renderingIds)
        {
            var existingRendringIds = GetActiveRenderigIds();
            var newRenderingIds = renderingIds.Where(r => existingRendringIds.All(er => er != r)).ToList();
            existingRendringIds.AddRange(newRenderingIds);
            SaveStatus(existingRendringIds);
        }
        public static void DeactivateRendering(List<ID> renderingIds)
        {
            var existingRenderingIds = GetActiveRenderigIds();
            var updatedList = existingRenderingIds.Where(r => renderingIds.All(dr => dr != r)).ToList(); 

            SaveStatus(updatedList);
        }

        public static List<ID> GetActiveRenderigIds()
        { 
            var item = GetStateItem();
            MultilistField stateFiled = item.Fields[Fields.StateItem.ActiveRenderings];
            return  stateFiled?.TargetIDs.ToList() ?? new List<ID>();
        }

        public static Item GetStateItem()
        {
            var db = Database.GetDatabase(Settings.DatabaseName);
            return db.GetItem(ItemIds.StatusItemId);
        }
    }
}