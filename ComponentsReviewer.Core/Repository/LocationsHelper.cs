using System.Collections.Generic;
using System.Linq;
using ComponentsReviewer.Models;
using Sitecore.Data;
using Sitecore.Data.Fields;
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
                Log.Error("ComponentsReviewer *** GetAllRenderings failed: the database {0} cannot be found".FormatWith(Settings.DatabaseName), typeof(LocationsHelper));
                return new List<Item>();
            }

            var idStr = Settings.RenderingPath;
            if (!ID.IsID(idStr))
            { 
                Log.Error("ComponentsReviewer *** GetAllRenderings failed: settings item id has wrong format", typeof(LocationsHelper));
                return new List<Item>();
            }


            var settingsItem = db.GetItem(new ID(idStr));
            if (settingsItem == null)
            {
                Log.Error("ComponentsReviewer *** GetAllRenderings failed: settings item cannot be found", typeof(LocationsHelper));
                return new List<Item>();
            }

            MultilistField roots = settingsItem.Fields[Fields.SettingsItem.RootItems];
            if (roots == null)
            {
                Log.Error("ComponentsReviewer *** GetAllRenderings failed: filed 'Root Items' is null", typeof(LocationsHelper));
                return new List<Item>();
            }

            return roots.GetItems().ToList(); 
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