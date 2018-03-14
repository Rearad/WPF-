using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace JavaPackage
{
    public class MavenElement
    {
        private string _packageName;
        private ImageSource _packageImage;
        private string _packageDescribe;
        private string _packageSubheading;
        private string _packageUpdatedDate;
        private Dictionary<string, string> _packagePath;
        private string _packageResultPath;

        public string PackageName { get => _packageName; set => _packageName = value; }
        public ImageSource PackageImage { get => _packageImage; set => _packageImage = value; }
        public string PackageDescribe { get => _packageDescribe; set => _packageDescribe = value; }
        public string PackageSubheading { get => _packageSubheading; set => _packageSubheading = value; }
        public string PackageUpdatedDate { get => _packageUpdatedDate; set => _packageUpdatedDate = value; }
        public Dictionary<string,string> PackagePath { get => _packagePath; set => _packagePath = value; }
        public string PackageResultPath { get => _packageResultPath; set => _packageResultPath = value; }
    }
}
