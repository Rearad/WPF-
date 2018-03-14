using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JavaPackage
{
    public class DownLoadFile
    {
        public DownLoadFile()
        {

        }

        /// <summary>
        /// 下载网址上的文件
        /// </summary>
        /// <param name="url">资源定位符</param>
        /// <param name="path">本地路径</param>
        public async Task DownFileAsync(string url, string path)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            int read = 0;
            byte[] buff = new byte[1024*1024*1];
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                while ((read = stream.Read(buff, 0, buff.Length)) > 0)
                {
                    await fileStream.WriteAsync(buff, 0, read);
                }
                fileStream.Flush();
            }
        }

        

    }

}
