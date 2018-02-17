using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Layouts;

namespace ComponentsReviewer.Repository
{
    public class LayoutsHelper
    {/// <summary>
        /// Checks if any of the sublayouts specified in 'sublayoutGuids' is present in the presentation of the 'currentItem'
        /// </summary>
        /// <param name="currentItem">The current item.</param>
        /// <param name="sublayoutGuids">The sublayout guids.</param>
        /// <returns>true / false</returns>
        public static bool AnySublayoutInPresentation(Item currentItem, List<string> sublayoutGuids)
        {
            var layout = LayoutDefinition.Parse(LayoutField.GetFieldValue(currentItem.Fields["__Renderings"]));

            if (layout.Devices != null && layout.Devices.Count > 0)
            {
                var dev = (DeviceDefinition)layout.Devices[0];
                if (dev != null)
                {
                    var renderings = dev.Renderings;
                    if (renderings != null)
                    {
                        if (renderings.Cast<RenderingDefinition>().Any(rend => sublayoutGuids.Contains(rend.ItemID)))
                        {
                            return true;
                        }
                    }

                    string renderingsHtml = "";
                    ((RenderingItem) dev.Renderings[0]).GetDesignTimeHtml(new NameValueCollection(), ref renderingsHtml);
                }
            }
            return false;
        }
    }
}