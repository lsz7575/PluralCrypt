using System.Collections.Generic;
using System.Linq;

namespace PluralCrypt.Model
{
    public class Module
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorHandle { get; set; }
        public int Index { get; set; }
        public List<Clip> Clips { get; set; }

        public bool IsDownloaded { get { return !Clips.Any(clp => !clp.IsDownloaded); } }

        public Module()
        {
            Clips = new List<Clip>();
        }
    }
}
