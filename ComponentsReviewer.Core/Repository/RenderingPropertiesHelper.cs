using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using ComponentsReviewer.Models;
using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Resources.Media;
using Sitecore.SecurityModel;
using Sitecore.StringExtensions;

namespace ComponentsReviewer.Repository
{
    public class RenderingPropertiesHelper
    {
        public static RenderingProperties GetRenderingsProperties(Item renderingItem)
        {
            var props = GetStandardProperties(renderingItem);
            if (props == null)
            {
                return null; 
            }

            if (props.TemplateId == TemplateIds.ViewRendering.Guid
                || props.TemplateId == TemplateIds.XmlRendering.Guid ||
                props.TemplateId == TemplateIds.Sublayout.Guid)
            {
                props.FilePath = renderingItem[Fields.Path];
            }

            if (props.TemplateId == TemplateIds.ControllerRendering.Guid)
            {
                props.FilePath = "controller: '{0}'  </br> action: '{1}'".FormatWith(renderingItem[Fields.Controller],
                    renderingItem[Fields.Action]);
            }

            SetRenderingStateProperties(props);

            return props; 
        }

        private static List<Link> GetLinks(Item renderingItem)
        {
            return LinksProvider.GetRefferencedItems(renderingItem);
        }

        private static RenderingProperties GetStandardProperties(Item renderingItem)
        {
            try
            {
                var links = GetLinks(renderingItem);
                var sites = links.Select(s => s.SiteName).Distinct().Where(c=>!string.IsNullOrEmpty(c)).ToList();
                var obj = new RenderingProperties
                {
                    Id = renderingItem.ID.Guid,
                    Name = renderingItem.Name,
                    DatasourcePath = renderingItem[Fields.DatasourceLocation],
                    TemplateId = renderingItem.TemplateID.Guid,
                    TemplateName = renderingItem.TemplateName,
                    TemplatePath = renderingItem.Template.FullName,
                    ItemPath = renderingItem.Paths.FullPath, 
                    Link = links.FirstOrDefault() ?? new Link{ItemName = "[Not Used]"},
                    LinksCount = links.Count,
                    Sites = sites,
                    ImageUrl = GetThumbnailUrl(renderingItem)
                };
                SetDatasourceData(renderingItem, obj);
                return obj;
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e);
                return null;
            } 
        }
        
        public static void SetRenderingStateProperties(RenderingProperties properties)
        {
            var stateItem = StateProvider.GetStateItem();
            if (stateItem == null)
            {
                Log.Error("rendering item is null", typeof(RenderingPropertiesHelper));
                return;
            }

            NameValueListField field = stateItem.Fields[Fields.StateItem.RenderingsProperties];
            var collection = field.NameValues;
            var renderingProperties = collection.GetValues(properties.Id.ToString())?.FirstOrDefault();
            if (renderingProperties == null)
            {
                return;
            }

            var indexComments = HttpUtility.UrlDecode(renderingProperties).IndexOf("|comments:", StringComparison.Ordinal);
            var bugStr = renderingProperties.Substring(7, 4);
            properties.Comments = HttpUtility.UrlDecode(renderingProperties.Substring(indexComments + 10));
             
            bool.TryParse(bugStr, out var hasBug);
            properties.HasBug = hasBug;
        }

        public static void SaveRenderingProperties(RenderingProperties properties)
        { 
            var stateItem = StateProvider.GetStateItem();
            if (stateItem == null)
            {
                Log.Error("rendering item is null", typeof(RenderingPropertiesHelper));
                return;
            }

            NameValueListField field = stateItem.Fields[Fields.StateItem.RenderingsProperties];
            var collection = field.NameValues; 

            collection.Remove(properties.Id.ToString());
            collection.Add(properties.Id.ToString(), "hasBug:{0}|comments:{1}".FormatWith(properties.HasBug, HttpUtility.UrlEncode(properties.Comments)));

            using (new SecurityDisabler())
            {
                stateItem.Editing.BeginEdit();
                try
                {
                    stateItem[Fields.StateItem.RenderingsProperties] = StringUtil.NameValuesToString(collection, "&");
                }
                finally
                {
                    stateItem.Editing.EndEdit();
                }
            }
        }

        private static NameValueCollection GetPropertiesFieldValue(NameValueListField field, Guid id)
        {
            var properties = field.NameValues.GetValues(id.ToString());
            var collection = new NameValueCollection();
            return field.NameValues;
        }

        private static void SetDatasourceData(Item renderingItem, RenderingProperties properties)
        {
            //var ds = renderingItem[Fields.DatasourceLocation];

            //properties.DatasourceTemplateId = ds.
        }

        private static string GetThumbnailUrl(Item renderingItem)
        {
            try
            {
                ImageField thumbField = renderingItem.Fields[Fields.Thumbnail];
                if (thumbField != null && thumbField.MediaItem != null)
                {
                    var image = new MediaItem(thumbField.MediaItem);
                    return MediaManager.GetMediaUrl(image);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }

            return string.Empty;
        }
    }
}