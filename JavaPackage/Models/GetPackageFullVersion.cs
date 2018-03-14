using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaPackage.Models
{
    /// <summary>
    /// 获取该Jar包的所有版本；
    /// </summary>
    class GetPackageFullVersion
    {

        

        public GetPackageFullVersion()
        {

        }
        
        public void GetFullPackage(string url)
        {
            WebElements webElements = new WebElements(url);
            
            webElements.InnerHtml.RemoveRange(0, 46);
            webElements.InnerText.RemoveRange(0, 46);

            string str = webElements.InnerHtml[0];
            webElements.InnerText.RemoveRange(webElements.InnerText.Count - 11, 11);
            webElements.InnerHtml.RemoveRange(webElements.InnerHtml.Count - 11, 11);

            PackageVersions versions = new PackageVersions();
            versions.PackageFullversion = new List<PackageVersionItem>();

            int num = 0;
            for (int i = 0; i < webElements.InnerHtml.Count; i++)
            {
                if (num == i)
                {
                    PackageVersionItem versionItem = new PackageVersionItem();
                    versionItem.VersionNumber = webElements.InnerText[i];
                    versionItem.VersionUrl = webElements.InnerHtml[i];
                    versions.PackageFullversion.Add(versionItem);
                    num += 2;
                }
            }
        }
    }
}
