using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaPackage.Models
{
    public class PackageVersions
    {
        
        private List<PackageVersionItem> _packageFullversion;
        /// <summary>
        /// 所有版本号的信息
        /// </summary>
        public List<PackageVersionItem> PackageFullversion { get => _packageFullversion; set => _packageFullversion = value; }
    }
}
