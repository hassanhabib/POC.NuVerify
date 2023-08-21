using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.NuVerify.Models.Nugets
{
    internal class NugetPackage
    {
        public Context context { get; set; }
        public int totalHits { get; set; }
        public Datum[] data { get; set; }
    }

    public class Context
    {
        public string vocab { get; set; }
        public string _base { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string type { get; set; }
        public string registration { get; set; }
        public string version { get; set; }
        public string description { get; set; }
        public string summary { get; set; }
        public string title { get; set; }
        public string iconUrl { get; set; }
        public string licenseUrl { get; set; }
        public string projectUrl { get; set; }
        public string[] tags { get; set; }
        public string[] authors { get; set; }
        public string[] owners { get; set; }
        public int totalDownloads { get; set; }
        public bool verified { get; set; }
        public Packagetype[] packageTypes { get; set; }
        public Version[] versions { get; set; }
        public object[] vulnerabilities { get; set; }
    }

    public class Packagetype
    {
        public string name { get; set; }
    }

    public class Version
    {
        public string version { get; set; }
        public int downloads { get; set; }
        public string id { get; set; }
    }
}

