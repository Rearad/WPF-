using JavaPackage.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JavaPackage
{
    #region Windows系统特性
    internal enum AccentState
    {
        ACCENT_DISABLED = 1,
        ACCENT_ENABLE_GRADIENT = 0,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        /// <summary>
        /// 1：背景色为黑色
        /// 2：背景色为白色
        /// 3：模糊背景
        /// 4：透明背景
        /// </summary>
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal enum WindowCompositionAttribute
    {
        // ...
        WCA_ACCENT_POLICY = 19
        // ...
    }
    #endregion

    /// <summary>
    /// JavaPackageMainPage.xaml 的交互逻辑
    /// </summary>
    public partial class JavaPackageMainPage : Window
    {
        private PackageItems _packTtem;
        private WebElements _webElement;
        public PackageItems PackTtem { get => _packTtem; set => _packTtem = value; }
        internal WebElements WebElement { get => _webElement; set => _webElement = value; }

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        public JavaPackageMainPage()
        {
            PackTtem = new PackageItems { GetMavenElement = new ObservableCollection<MavenElement>() };
            InitializeComponent();
        }

        #region 逻辑代码
        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy
            {
                AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND
            };

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };
            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }


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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                EnableBlur();

                UploadPackage();
            }
            catch (Exception)
            {

            }


        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tb_search.Text.Length > 0)
            {
                tb_search.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                tb_search.Clear();
            }

        }

        private void Search_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tb_search.Text.Length == 0)
            {
                FormattingTb(tb_search);
            }
        }
        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Equals("Return") || e.Key.Equals("Enter"))
            {
                UploadPackage();
                but_affirm.Focus();
            }
        }

        public void FormattingTb(TextBox textBox)
        {
            textBox.Foreground = new SolidColorBrush(Color.FromArgb(255, 102, 102, 102));
            textBox.Text = "搜索你想要的jar包";
        }

        private void Affirm_Click(object sender, RoutedEventArgs e)
        {
            UploadPackage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        public void UploadPackage()
        {
            Task task = new Task(() =>
            {
                Dispatcher dispatcher = lv_htmlContext.Dispatcher;
                ThreadPool.QueueUserWorkItem(delegate
                {
                    dispatcher.Invoke(() =>
                    {
                        PackTtem.GetMavenElement.Clear();
                        if (tb_search.Text.Equals("搜索你想要的jar包") || tb_search.Text.Length == 0)
                        {
                            LoadPackageAllData();
                        }
                        else
                        {
                            LoadSearchData();
                        }
                    });
                    Thread.Sleep(100);
                });
            });
            task.Start();
        }
        #endregion

        /// <summary>
        /// 加载搜索后的结果
        /// </summary>
        private void LoadSearchData()
        {
            Task task = new Task(() =>
            {
                try
                {
                    Dispatcher dispatcher = lv_htmlContext.Dispatcher;
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        dispatcher.Invoke(() =>
                        {
                            try
                            {
                                MavenItems package = new MavenItems("http://www.mvnrepository.com/search?q=" + tb_search.Text, true);
                                if (package.PackageImage.Count > 0)
                                {
                                    for (int i = 0; i < package.PackageImage.Count; i++)
                                    {
                                        MavenElement maven = new MavenElement()
                                        {
                                            PackageName = package.PackageName[i],
                                            PackageImage = new BitmapImage(new Uri(package.PackageImage[i])),
                                            PackageDescribe = package.PackageSubheading[i] + "  »  " + package.PackageUpdatedDate[i],
                                            PackageSubheading = "下载次数" + package.PackageDescribe[i],
                                            PackageResultPath = "http://www.mvnrepository.com" + package.PackageDownUrl[i]
                                        };
                                        maven.PackagePath = package.PackagePaging;
                                        PackTtem.GetMavenElement.Add(maven);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("未找到此资源");
                                }
                            }
                            catch (Exception e)
                            {
                                //MessageBox.Show(e.Message);
                            }
                        });
                    });


                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message);
                }
            });
            task.Start();
        }

        /// <summary>
        /// 加载所有的结果
        /// </summary>
        private void LoadPackageAllData()
        {
            Task task = new Task(() =>
            {
                try
                {
                    Dispatcher dispatcher = lv_htmlContext.Dispatcher;
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        dispatcher.Invoke(() =>
                        {
                            try
                            {
                                MavenItems package = new MavenItems("http://www.mvnrepository.com", false);
                                if (package.PackageImage.Count > 0)
                                {
                                    for (int i = 0; i < package.PackageImage.Count; i++)
                                    {
                                        MavenElement maven = new MavenElement()
                                        {
                                            PackageName = package.PackageName[i],
                                            PackageImage = new BitmapImage(new Uri(package.PackageImage[i])),
                                            PackageDescribe = package.PackageSubheading[i] + "  »  " + package.PackageUpdatedDate[i],
                                            PackageSubheading = "下载次数" + package.PackageDescribe[i],
                                            PackageResultPath = "http://www.mvnrepository.com" + package.PackageDownUrl[i]
                                        };
                                        PackTtem.GetMavenElement.Add(maven);
                                    }
                                }
                                else
                                {
                                    //MessageBox.Show("未正常加载数据，你可以点击刷新重试");
                                }
                            }
                            catch (Exception loadAllException)
                            {
                                MessageBox.Show(loadAllException.Message);
                            }
                        });
                    });
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            });
            task.Start();
        }
        private void CalendarDayButton_MouseLeave(object sender, MouseEventArgs e)
        {
            tb_search.Focus();
        }
        //bool radioButtonIsChecked = true;
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            //RadioButton radioButton = sender as RadioButton;
            //if (radioButtonIsChecked)
            //{
            //    radioButton.IsChecked = true;
            //    lv_htmlContext.SelectionMode = SelectionMode.Extended;
            //    MessageBox.Show("按住上档键选择多个Jar包");
            //    radioButtonIsChecked = false;
            //}
            //else
            //{
            //    radioButton.IsChecked = false;
            //    lv_htmlContext.SelectionMode = SelectionMode.Single;
            //    MessageBox.Show("已取消多选");
            //    radioButtonIsChecked = true;
            //}
        }
        private DownLoading downLoading;
        private void CalendarDayButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            //lv_htmlContext.SelectedIndex = -1;
            //DownLoadFile loadFile = new DownLoadFile();
            //await loadFile.DownFileAsync("http://pic4.nipic.com/20091217/3885730_124701000519_2.jpg", @"E:\我的文件夹\1.jpg");
            if (downLoading == null || !downLoading.IsVisible) { downLoading = new DownLoading(); }
            downLoading.Show();
            try
            {
                bool b = true;
                MavenElement maven = lv_htmlContext.SelectedItem as MavenElement;
                if (JarItems.LoadMavenElementItems.Count > 0)
                {
                    foreach (MavenElement mavenElementitem in JarItems.LoadMavenElementItems)
                    {
                        if (maven.PackageResultPath.Equals(mavenElementitem.PackageResultPath))
                        {
                            b = false;
                            break;
                        }
                    }
                }
                if (b)
                {
                    JarItems.LoadMavenElementItems.Add(maven);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        //List<MavenElement> mavenElement = new List<MavenElement>();
        //GetPackageFullVersion getPackage = new GetPackageFullVersion();
        private void lv_htmlContext_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //bool b = true;
            //if (e.AddedItems.Count > 0)
            //{
            //    foreach (MavenElement item in e.AddedItems)
            //    {
            //        if (JarItems.LoadMavenElementItems.Count > 0)
            //        {
            //            foreach (MavenElement mavenElementitem in JarItems.LoadMavenElementItems)
            //            {
            //                if (item.PackageResultPath.Equals(mavenElementitem.PackageResultPath))
            //                {
            //                    b = false;
            //                    break;
            //                }
            //            }
            //        }
            //        if (b)
            //        {
            //            JarItems.LoadMavenElementItems.Add(item);
            //        }
            //    }
            //}
        }
    }
}
