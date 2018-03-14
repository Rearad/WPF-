using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace JavaPackage
{
    class MavenItems
    {

        private List<string> _packageDownUrl;
        private List<string> _packageImage;
        private List<string> _packageName;
        private List<string> _packageDescribe;
        private List<string> _packageSubheading;
        private List<string> _packageUpdatedDate;
        //private 
        private Dictionary<string, string> _packagePaging;

        private WebElements _getWebElements;
        public List<string> PackageImage { get => _packageImage; set => _packageImage = value; }
        public List<string> PackageName { get => _packageName; set => _packageName = value; }
        public List<string> PackageDescribe { get => _packageDescribe; set => _packageDescribe = value; }
        public List<string> PackageSubheading { get => _packageSubheading; set => _packageSubheading = value; }
        internal WebElements GetWebElements { get => _getWebElements; set => _getWebElements = value; }
        public List<string> PackageUpdatedDate { get => _packageUpdatedDate; set => _packageUpdatedDate = value; }
        public Dictionary<string, string> PackagePaging { get => _packagePaging; set => _packagePaging = value; }
        public List<string> PackageDownUrl { get => _packageDownUrl; set => _packageDownUrl = value; }

        public MavenItems(string website, bool IsGrabble)
        {
            try
            {
                if (GetWebElements == null)
                {
                    GetWebElements = new WebElements(website);
                }
                GetImage();
                GetRests(IsGrabble);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        private void GetImage()
        {
            GetWebElements.ImagePath.RemoveRange(0, 2);
            PackageImage = GetWebElements.ImagePath;
        }
        private void GetRests(bool IsGrabble)
        {
            if (IsGrabble) { SelectByName(); }
            else { SelectDefault(); }
        }

        #region 根据输入查找需要的包  SelectByName()
        /// <summary>
        /// 根据输入查找需要的包
        /// </summary>
        public void SelectByName()
        {
            GetWebElements.InnerHtml.RemoveRange(0, 40);
            GetWebElements.InnerText.RemoveRange(0, 40);
            if (PackageUpdatedDate == null)
            {
                PackagePaging = new Dictionary<string, string>();
            }
            if (PackageName == null)
            {
                PackageName = new List<string>();
            }
            if (PackageSubheading == null)
            {
                PackageSubheading = new List<string>();
            }
            if (PackageUpdatedDate == null)
            {
                PackageUpdatedDate = new List<string>();
            }
            if (PackageDescribe == null)
            {
                PackageDescribe = new List<string>();
            }
            if (PackageDownUrl == null)
            {
                PackageDownUrl = new List<string>();
            }

            PackagePaging.Clear();
            PackageName.Clear();
            PackageSubheading.Clear();
            PackageUpdatedDate.Clear();
            PackageDescribe.Clear();
            PackageDownUrl.Clear();

            if (PackageImage != null && PackageImage.Count > 0)
            {
                GetWebElements.InnerHtml.RemoveRange(GetWebElements.InnerHtml.Count - 10, 10);
                GetWebElements.InnerText.RemoveRange(GetWebElements.InnerText.Count - 10, 10);
                if (GetWebElements.InnerHtml.Count > 20)
                {
                    try
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            string name = GetWebElements.InnerText[GetWebElements.InnerText.Count - 1 - i];
                            string txt = GetWebElements.InnerHtml[GetWebElements.InnerHtml.Count - 1 - i];
                            PackagePaging.Add(name, txt);
                        }
                        GetWebElements.InnerHtml.RemoveRange(GetWebElements.InnerHtml.Count - 10, 10);
                        GetWebElements.InnerText.RemoveRange(GetWebElements.InnerText.Count - 10, 10);
                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.Message);
                    }

                }
                //GetWebElements.InnerHtml.RemoveRange(GetWebElements.InnerHtml.Count - 10, 10);
                //GetWebElements.InnerText.RemoveRange(GetWebElements.InnerText.Count - 10, 10);

                try
                {
                    int num1 = 1;
                    int num2 = 2;
                    int num3 = 3;
                    int num4 = 4;
                    for (int i = 1; i <= GetWebElements.InnerText.Count; i++)
                    {
                        if (num1 == i)
                        {
                            PackageName.Add(GetWebElements.InnerText[i]);
                            PackageDownUrl.Add(GetWebElements.InnerHtml[i]);
                            num1 += 5;
                        }
                        if (num2 == i)
                        {
                            PackageDescribe.Add(GetWebElements.InnerText[i]);
                            num2 += 5;
                        }
                        if (num3 == i)
                        {
                            PackageSubheading.Add(GetWebElements.InnerText[i]);
                            num3 += 5;
                        }
                        if (num4 == i)
                        {
                            PackageUpdatedDate.Add(GetWebElements.InnerText[i]);
                            num4 += 5;
                        }
                    }
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message);
                }

            }
        }
        #endregion

        /// <summary>
        /// 查询默认的数据
        /// </summary>
        private void SelectDefault()
        {
            GetWebElements.InnerHtml.RemoveRange(0, 38);
            GetWebElements.InnerText.RemoveRange(0, 38);
            if (PackageUpdatedDate == null)
            {
                PackagePaging = new Dictionary<string, string>();
            }
            if (PackageName == null)
            {
                PackageName = new List<string>();
            }
            if (PackageSubheading == null)
            {
                PackageSubheading = new List<string>();
            }
            if (PackageUpdatedDate == null)
            {
                PackageUpdatedDate = new List<string>();
            }
            if (PackageDescribe == null)
            {
                PackageDescribe = new List<string>();
            }
            if (PackageDownUrl == null)
            {
                PackageDownUrl = new List<string>();
            }

            PackagePaging.Clear();
            PackageName.Clear();
            PackageSubheading.Clear();
            PackageUpdatedDate.Clear();
            PackageDescribe.Clear();
            PackageDownUrl.Clear();

            GetWebElements.InnerHtml.RemoveRange(GetWebElements.InnerHtml.Count - 21, 21);
            GetWebElements.InnerText.RemoveRange(GetWebElements.InnerText.Count - 21, 21);


            try
            {
                int num1 = 1;
                int num2 = 2;
                int num3 = 3;
                int num4 = 4;
                for (int i = 1; i <= GetWebElements.InnerText.Count; i++)
                {
                    if (num1 == i)
                    {
                        PackageName.Add(GetWebElements.InnerText[i]);
                        PackageDownUrl.Add(GetWebElements.InnerHtml[i]);
                        num1 += 6;
                    }
                    if (num2 == i)
                    {
                        PackageDescribe.Add(GetWebElements.InnerText[i]);
                        num2 += 6;
                    }
                    if (num3 == i)
                    {
                        PackageSubheading.Add(GetWebElements.InnerText[i]);
                        num3 += 6;
                    }
                    if (num4 == i)
                    {
                        PackageUpdatedDate.Add(GetWebElements.InnerText[i]);
                        num4 += 6;
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }

        }
    }
}
