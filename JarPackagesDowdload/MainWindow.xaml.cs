using JarPackagesDowdloads;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JarPackagesDowdload
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
            //Say();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Graphics.FromImage(Resources.logo_banner, new System.Drawing.Point(150, 80));


        }



        //public void Say()
        //{
        //    Border border = new Border();
        //    VisualBrush brush = new VisualBrush
        //    {
        //        Visual = image_border,
        //        Stretch = Stretch.Uniform
        //    };
        //    border.Background = brush;
        //    border.Effect = new BlurEffect()
        //    {
        //        Radius = 80,
        //        RenderingBias = RenderingBias.Performance
        //    };
        //    border.Margin = new Thickness(-this.Margin.Left, -this.Margin.Top, 0, 0);

        //    grid_image.ClipToBounds = true;
        //    grid_image.Children.Clear();
        //    grid_image.Children.Add(border);
        //    //this.ClipToBounds = true;
        //    //this.Children.Clear();
        //    //this.Children.Add(border);
        //}

    }
}
