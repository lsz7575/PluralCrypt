using System.Collections.Generic;

namespace DecryptPluralSightVideos.Model
{
    public class Course
    {
        public string CourseName { get; set; }
        public string CourseTitle { get; set; }
        public int HasTranscript { get; set; }
        public List<Module> Modules { get; set; }

        public Course()
        {
            Modules = new List<Module>();
        }
    }
}
