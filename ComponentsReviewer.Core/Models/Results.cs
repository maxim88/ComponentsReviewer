using System.Collections.Generic;

namespace ComponentsReviewer.Models
{
    public class Results
    {
        public List<RenderingProperties> Renderings { get; set; }
        public List<Location> Epics { get; set; } 
        public List<Location> ExpectedFeatures { get; set; }
    }
}