using DMSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace JarPackagesDowdload.cs
{
    public class Item : MyControlItem
    {
        public bool ImageMouseHover;
        public bool InfoHover;

        public string ID { get; set; }

        public System.Drawing.Image Image { get; set; }

        public System.Drawing.Rectangle ImageBo { get; set; }

        public string Info { get; set; }

        public System.Drawing.Rectangle InfoBo { get; set; }
    }
}
