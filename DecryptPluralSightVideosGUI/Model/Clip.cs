using System.Collections.Generic;

namespace DecryptPluralSightVideosGUI.Model
{
    public class Clip
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public int Index { get; set; }
        public bool IsDownloaded { get; set; }
        public List<ClipTranscript> Subtitle { get; set; }

        public Clip()
        {
            Subtitle = new List<ClipTranscript>();
        }
    }
}
