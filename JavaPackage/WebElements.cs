using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace JavaPackage
{
    class WebElements
    {
        /// <summary>
        /// A标签的地址集合
        /// </summary>
        public List<string> InnerHtml { get; set; }
        /// <summary>
        /// A标签的文本集合
        /// </summary>
        public List<string> InnerText { get; set; }
        //所有的图片的集合
        public List<string> ImagePath { get; set; }
        private string HtmlUrl { get; set; }
        /// <summary>
        /// 整个网页的文本
        /// </summary>
        public string HtmlDocment { get; set; }

        public WebElements(string url)
        {
            try
            {
                this.HtmlUrl = url;
                GetHtmlText();
                PickupImgUrl(HtmlDocment);
                GetHtmlElementA(HtmlDocment);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #region 获取网页内容 GetHtmlText()
        /// <summary>
        /// 获取网页内容
        /// </summary>
        /// <param name="url">输入要获取的网址</param>
        /// <returns>返回一个StringBuilder类型结果</returns>
        private void GetHtmlText()
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(HtmlUrl);
                using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);
                        HtmlDocment = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        #endregion

        #region 获取网页内容的图片地址 PickupImgUrl(string html)
        /// <summary>
        /// 获取网页内容的图片地址
        /// </summary>
        /// <param name="html">要传入的html文档</param>
        /// <returns></returns>
        private void PickupImgUrl(string html)
        {
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            MatchCollection matches = regImg.Matches(html);
            if (ImagePath == null)
            {
                ImagePath = new List<string>();
            }
            foreach (Match match in matches)
            {
                ImagePath.Add(match.Groups["imgUrl"].Value);
            }
        }
        #endregion

        #region 获取html中的所有A标签  GetHtmlElementA(string html)
        /// <summary>
        /// 获取html中的所有A标签
        /// </summary>
        /// <param name="html">要查找的HTML文本</param>
        private void GetHtmlElementA(string html)
        {
            try
            {
                InnerHtml = new List<string>();
                InnerText = new List<string>();

                if (InnerHtml.Count > 0) { InnerHtml.Clear(); };
                if (InnerText.Count > 0) { InnerText.Clear(); };
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                Regex reg = new Regex(@"(?is)<a(?:(?!href=).)*href=(['""]?)(?<url>[^""\s>]*)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>");
                MatchCollection mc = reg.Matches(html);
                foreach (Match m in mc)
                {
                    InnerText.Add(m.Groups["text"].Value);
                    InnerHtml.Add(m.Groups["url"].Value);
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        #endregion

        [DllImport("User32.dll", EntryPoint = "SystemParametersInfo")]
        private static extern bool SysemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        #region 设置桌面壁纸 SetWallpaper(string path)
        /// <summary>
        /// 设置桌面壁纸
        /// </summary>
        /// <param name="path">图片路径</param>
        public static void SetWallpaper(string path)
        {
            SysemParametersInfo(20, 0, path, 0x01 | 0x02);
        }
        #endregion


    }
}
