using System;
using System.Collections.Generic;
using System.Text;

namespace TVTKWindows
{
    public enum Type
    {
        Video,
        Document,
        Photo
    }

    public class MultimediaFile
    {
        // public Url url { get; set; }
        public Type type { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
    }
}
