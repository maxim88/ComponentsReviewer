using System.Collections.Generic;
using System.Linq;
using ComponentsReviewer.Models;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.StringExtensions;

namespace ComponentsReviewer.Repository
{
    public class LocationsHelper
    {
        public static LocationsObj locations = null;

        public static LocationsObj Locations { get; set; }
         
        public static List<Item> GetRootItems()
        {
            var db = Database.GetDatabase(Settings.DatabaseName);
            if (db == null)
            {
                Log.Error("GetAllRenderings failed: the database {0} cannot be found".FormatWith(Settings.DatabaseName), typeof(RenderingsGetter));
                return new List<Item>();
            }
            
            return Settings.RenderingPath.Split(',').Select(id => db.GetItem(id)).Where(item => item != null).ToList();
        } 

        public static List<Location> GetEpics()
        {
            var epics = new List<Location>(); 
            var roots = GetRootItems();
            foreach (var root in roots)
            {
                epics.Add(new Location
                {
                    Path = root.Paths.FullPath,
                    Name = root.Name,
                    Id = root.ID.Guid,
                    ChildLocations = root.Children.Select(c=>new Location
                    {
                        Path = c.Paths.FullPath,
                        Name = c.Name,
                        Id = c.ID.Guid,
                    }).ToList()
                });
            }

            return epics;
        }
    }
}