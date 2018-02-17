using System.Collections.Generic;

namespace ComponentsReviewer.Models
{
    public class LocationsObj
    {
        public List<Location> Epics { get; set; }
        public List<Location> ExpectedFeatures { get; set; }
        public List<Location> Features { get; set; }
    }
}