using System;
using System.Collections.Generic;

namespace ComponentsReviewer.Models
{
    public class RenderingProperties
    {
        public bool HasBug { get; set; } 
        public string Comments { get; set; } 
        public bool IsActive { get; set; } 
        public string FilePath { get; set; }
        public string ItemPath { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DatasourcePath { get; set; }
        public string DatasourceTemplateName { get; set; } 
        public Guid DatasourceTemplateId { get; set; } 
        public Guid TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplatePath { get; set; }
        public Guid ParametersTemplateId { get; set; }
        public Guid ParametersTemplateName { get; set; }
        public List<Link> Links { get; set; }
        public Link Link { get; set; }
        public int LinksCount { get; set; }
        public string ImageUrl { get; set; }
        public string Epic { get; set; }
        public string Feature { get; set; }
        public List<string> Sites { get; set; }
        public string ExpectedFeature { get; set; }
    }
}