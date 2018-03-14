using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JavaPackage
{
    /// <summary>
    /// DownLoading.xaml 的交互逻辑
    /// </summary>
    public partial class DownLoading : Window
    {
        public DownLoading()
        {
            InitializeComponent();
            backgroundHtml.Navigate(new Uri(@"D:\Program Files\wallpaper_engine\projects\defaultprojects\3DMGAME-Wallpaper.888689688\index.html"));
        }

        private void hhh_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(JarItems.LoadMavenElementItems.ToString());
            
        }

        #region 鼠标按下可以移动窗体
        /// <summary>
        /// 鼠标按下可以移动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region 关闭当前窗体
        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        #endregion

        private void backgroundHtml_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
            //MyWebBrowser.Navigating += new NavigatingCancelEventHandler(MyWebBrowser_Navigating);
            //MyWebBrowser.Navigated += new NavigatedEventHandler(MyWebBrowser_Navigated);
            //Uri uri = new Uri("C:\\Users\\Manthravadi\\Desktop\\lobsterpothtml5pivotviewer-0.6.8\\examples\\PASS Summit 2011.html", UriKind.Absolute);
            backgroundHtml.Source = new Uri(@"D:\Program Files\wallpaper_engine\projects\defaultprojects\3DMGAME-Wallpaper.888689688\index.html", UriKind.Absolute);
        }
    }
}
