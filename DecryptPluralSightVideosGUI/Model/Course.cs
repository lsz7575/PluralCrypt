using System.Collections.Generic;

namespace DecryptPluralSightVideosGUI.Model
{
    public class Course
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int HasTranscript { get; set; }
        public List<Module> Modules { get; set; }

        public int NumOfVideo { get; set; }

        public Course()
        {
            Modules = new List<Module>();
        }
    }
}
