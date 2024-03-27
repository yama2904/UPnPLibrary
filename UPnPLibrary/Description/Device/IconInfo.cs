using System.Collections.Generic;

namespace UPnPLibrary.Description.Device
{
    public class IconInfo
    {
        public string Mimetype { get;  set; }


        public string Width { get;  set; }


        public string Height { get;  set; }


        public string Depth { get;  set; }


        public string Url { get;  set; }

        private const string MIME_TYPE = "mimetype";
        private const string WIDTH = "width";
        private const string HEIGHT = "height";
        private const string DEPTH = "depth";
        private const string URL = "url";

        public IconInfo() 
        {
        }

        public IconInfo(string mimetype, string width, string height, string depth, string url)
        {
            Mimetype = mimetype;
            Width = width;
            Height = height;
            Depth = depth;
            Url = url;
        }


        public IconInfo(Dictionary<string, string> map)
        {
            if (map.ContainsKey(MIME_TYPE))
            {
                Mimetype = map[MIME_TYPE];
            }

            if (map.ContainsKey(WIDTH))
            {
                Width = map[WIDTH];
            }

            if (map.ContainsKey(HEIGHT))
            {
                Height = map[HEIGHT];
            }

            if (map.ContainsKey(DEPTH))
            {
                Depth = map[DEPTH];
            }

            if (map.ContainsKey(URL))
            {
                Url = map[URL];
            }
        }
    }
}
