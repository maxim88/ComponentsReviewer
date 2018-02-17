using System;
using System.Collections.Generic;

namespace ComponentsReviewer.Models
{
    public class Location
    { 
        public List<Location> ChildLocations { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid Id { get; set; }
        public Guid RootId { get; set; }
    }
}