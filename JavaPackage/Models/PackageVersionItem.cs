using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaPackage.Models
{
    /// <summary>
    /// jar包中的版本信息
    /// </summary>
    public class PackageVersionItem
    {
        
        private string _versionNumber;
        private string _versionUrl;
        
        /// <summary>
        /// jar包版本号
        /// </summary>
        public string VersionNumber { get => _versionNumber; set => _versionNumber = value; }
        /// <summary>
        /// jar包地址
        /// </summary>
        public string VersionUrl { get => _versionUrl; set => _versionUrl = value; }
    }
}
