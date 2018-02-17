using System;

namespace ComponentsReviewer.Models
{
    public class Link
    {
        public string Url { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public string Language { get; set; }
        public string SiteName { get; set; }
    }
}