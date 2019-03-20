using System.Collections.Generic;

namespace DecryptPluralSightVideosGUI.Model
{
    public class Module
    {
        public string ModuleName { get; set; }
        public int ModuleId { get; set; }
        public string ModuleTitle { get; set; }
        public string AuthorHandle { get; set; }
        public int ModuleIndex { get; set; }
        public List<Clip> Clips { get; set; }
        public Module()
        {
            Clips = new List<Clip>();
        }
    }
}
