using System;

namespace VC.Common.Publishing
{
    [Serializable]
    public class ThunderstoreManifest
    {
        public string name;
        public string version_number;
        public string description;
        public string website_url;
        public string[] dependencies;
    }
}