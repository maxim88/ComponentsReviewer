using System;
using System.Collections.Concurrent;
using ComponentsReviewer.Models;

namespace ComponentsReviewer.Repository
{
    public class Conext
    {
        public Guid CurrentRenderingId { get; set; }
        public readonly ConcurrentDictionary<Guid, RenderingProperties> LoadedRenderings = new ConcurrentDictionary<Guid, RenderingProperties>();

        public void Reset()
        {
            LoadedRenderings.Clear();
            CurrentRenderingId = Guid.Empty;
        }

        
    }
}