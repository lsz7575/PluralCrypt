using System.Collections.Generic;
using System.Linq;

namespace DecryptPluralSightVideosGUI.Model
{
    public class Course
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public bool HasTranscript { get; set; }
        public List<Module> Modules { get; set; }

        public bool IsDownloaded { get { return !Modules.Any(md => !md.IsDownloaded); } }

        public Course()
        {
            Modules = new List<Module>();
        }
    }
}
