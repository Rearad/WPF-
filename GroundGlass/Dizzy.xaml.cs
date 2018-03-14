using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GroundGlass
{
    internal enum AccentState
    {
        ACCENT_DISABLED = 1,
        ACCENT_ENABLE_GRADIENT = 0,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
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
    /// <summary>
    /// Dizzy.xaml 的交互逻辑
    /// </summary>
    public partial class Dizzy : Window
    {

        public PackageDetails packageDetails { get; set; }

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        public Dizzy()
        {
            packageDetails = new PackageDetails();
            packageDetails.context = new List<Details>();

            for (int i = 0; i < 256; i++)
            {
                Details details = new Details();
                details.PackageImage = new BitmapImage(new Uri(@"D:\Garbage Code\Visual Studio 2017\WPF小应用\GroundGlass\image\354611-105.jpg"));
                details.PackageName = new TextBlock {
                    Text = "数据绑定",
                    FontSize = 32
                };

                packageDetails.context.Add(details);




                //ListView list = new ListView();
                //list.Background = new SolidColorBrush(Color.FromArgb(00, 255, 0, 255));
                //list.Width = 550;

                //Image image = new Image();
                //image.Source = new BitmapImage(new Uri(@"D:\Garbage Code\Visual Studio 2017\WPF小应用\GroundGlass\image\354611-105.jpg"));
                //image.Width = 32;
                //image.Height = 32;

                //list.Items.Add(image);


                //TextBlock textBlock = new TextBlock();
                //textBlock.Text = "数据绑定";
                //textBlock.FontSize = 32;
                //textBlock.TextAlignment = TextAlignment.Right;
                //textBlock.FontFamily = new FontFamily("Source Code Pro");
                //textBlock.Background = new SolidColorBrush(Color.FromArgb(00,255,0,255));
                //list.Items.Add(textBlock);
                //packageDetails.context.Add(list);


                //< Image Source = "{StaticResource itemImage}" Margin = "0,0,317.333,0" />
   
                //   < TextBlock 
                //           Text = "{StaticResource packageName}" />
                //< TextBlock Height = "49" Margin = "75.667,25,0,0" TextWrapping = "Wrap" VerticalAlignment = "Top" >
       
                //           < Run Text = "{StaticResource packageDescribe}" />< LineBreak />
        
                //            < Run Text = "{StaticResource packageSubheading}" />< LineBreak />
         
                //             < Run Text = "{StaticResource packageUpdatedDate}" />
          
                //          </ TextBlock >
          
                //      </ Grid >


            }
            //packageDetails.context = "这个是绑定的资源";
            //for (int i = 0; i < 140; i++)
            //{
            //    TextBlock textBlock = new TextBlock();

            //    Times.GetTextBlock.Text = "sdfssdf";
            //}
            InitializeComponent();

            
            //for (int i = 0; i < 200; i++)
            //{
            //    ListViewItem list = new ListViewItem();
            //    TextBlock text = new TextBlock();
            //    text.Text = "这是循环的" + i + "次";
            //    text.FontSize = 32;
            //    text.Background = new SolidColorBrush(Color.FromArgb(255,255,0,255));
            //    list.Content = text;
            //    Times.GetTextBlock = list;
            //}


        }

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            //times.GetDateList = new List<string>();
            //for (int i = 0; i < 200; i++)
            //{
            //    times.GetDateList.Add(i + "次");
            //}

            for (int i = 0; i < viewItems.Items.Count; i++)
            {

            }
        }
    }

    public class PackageDetails
    {
        public List<Details> context { get; set; }
        
        
    }

    public class Details
    {
        private TextBlock _packageName;
        private ImageSource _packageImage;
        public TextBlock PackageName { get => _packageName; set => _packageName = value; }
        public ImageSource PackageImage { get => _packageImage; set => _packageImage = value; }

        //public Details()
        //{
        //    ListView list = new ListView();
        //    Image image = new Image();
        //    image.Source = new BitmapImage(new Uri(@"D:\Garbage Code\Visual Studio 2017\WPF小应用\GroundGlass\image\354611-105.jpg"));
            
        //    list.Items.Add(image);


        //    TextBlock textBlock = new TextBlock();
        //    textBlock.Text = "数据绑定";
        //    textBlock.FontSize = 32;

        //    list.Items.Add(textBlock);
        //}
    }
}
