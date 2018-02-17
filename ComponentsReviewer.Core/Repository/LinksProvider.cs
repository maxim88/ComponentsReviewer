using System;
using System.Collections.Generic;
using System.Linq;
using ComponentsReviewer.Models;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.StringExtensions;
using Sitecore.Web;

namespace ComponentsReviewer.Repository
{
    public class LinksProvider
    { 
        public static List<Link> GetRefferencedItems(Guid renderingId)
        {
            var database = Database.GetDatabase(Settings.DatabaseName);
            if (database == null)
            {
                Log.Error("the database '{0}' cannot be found".FormatWith(Settings.DatabaseName), typeof(RenderingsGetter));
                return new List<Link>();
            }

            var renderingItem = database.GetItem(new ID(renderingId));
            if (renderingItem == null)
            {
                Log.Error("GetRenderingLinks failed: rendering '{0}' item cannot be found in the database '{1}'".FormatWith(renderingId, Settings.DatabaseName), typeof(RenderingsGetter));
                return new List<Link>();
            }

            return LinksProvider.GetRefferencedItems(renderingItem);
        }

        public static List<Link> GetRefferencedItems(Item item)
        {
            var list = new List<Link>();
            ItemLink[] itemLinks = Globals.LinkDatabase.GetReferrers(item);
            if (itemLinks == null)
            {
                return new List<Link>();
            }

            foreach (var itemLink in itemLinks.Where(d=>d.SourceDatabaseName == item.Database.Name))
            { 
                var sourceId = itemLink.SourceItemID;
                var sourceItem = item.Database.GetItem(sourceId);
                if (sourceItem == null)
                {
                    continue; 
                }

                var site = GetSite(sourceItem);
                var siteName = site == null ? string.Empty : site.Name;
                var url = LinkManager.GetItemUrl(sourceItem);
                var link = new Link
                {
                    ItemId = sourceId.Guid,
                    ItemName = sourceItem.Name,
                    Language = itemLink.SourceItemLanguage.Name,
                    SiteName = siteName,
                    Url = url
                };

                list.Add(link);
            }

            return list;
        }

        public static SiteInfo GetSite(Item item)
        {
            var siteInfoList = Sitecore.Configuration.Factory.GetSiteInfoList();

            SiteInfo currentSiteinfo = null;
            var matchLength = 0;
            foreach (var siteInfo in siteInfoList)
            {
                var path = siteInfo.RootPath + siteInfo.StartItem;

                if (item.Paths.FullPath.StartsWith(path, StringComparison.OrdinalIgnoreCase) && path.Length > matchLength)
                {
                    matchLength = siteInfo.RootPath.Length;
                    currentSiteinfo = siteInfo;
                }
            }

            return currentSiteinfo;
        } 
    }
}