using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSkin.Controls;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DMSkin.DirectUI;
using System.Drawing.Design;

namespace JarPackagesDowdload.cs
{
    public class MyControl : DMSkin.Controls.MyControl
    {
        private IContainer components;

        public event EventHandler InfoClick;

        public MyControl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void DrawItem(Graphics g, MyControlItem item, Rectangle rectItem)
        {
            if (item is Item)
            {
                if (((Item)item).Image != null)
                {
                    if (item.Equals(base.m_mouseOnItem))
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(30, 0xff, 0xff, 0xff)), item.Bounds);
                    }
                    g.DrawImage(((Item)item).Image, ((Item)item).ImageBo);
                    if (((Item)item).ImageMouseHover)
                    {
                        g.DrawRectangle(new Pen(Color.Aqua), ((Item)item).ImageBo);
                    }
                    if (((Item)item).InfoHover)
                    {
                        Font font = new Font(this.Font, FontStyle.Underline);
                        g.DrawString(((Item)item).Info, font, new SolidBrush(this.ForeColor), ((Item)item).InfoBo);
                        this.Cursor = Cursors.Hand;
                    }
                    else
                    {
                        g.DrawString(((Item)item).Info, this.Font, new SolidBrush(this.ForeColor), ((Item)item).InfoBo);
                    }
                    g.DrawString(((Item)item).ID, this.Font, new SolidBrush(this.ForeColor), new Rectangle(item.Bounds.X + 0x4b, item.Bounds.Y + 0x30, item.Bounds.Width - 0x4b, item.Bounds.Height - 0x30));
                }
            }
            else
            {
                base.DrawItem(g, item, rectItem);
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        protected override void OnClick(EventArgs e)
        {
            foreach (Item item in this.Items)
            {
                if (!item.ImageBo.Contains(base.m_ptMousePos) && item.InfoBo.Contains(base.m_ptMousePos))
                {
                    this.InfoClick?.Invoke(item, e);
                    break;
                }
            }
            base.OnClick(e);
        }

        protected override void OnCreateControl()
        {
            Graphics graphics = base.CreateGraphics();
            for (int i = 0; i < base.items.Count; i++)
            {
                ((Item)base.items[i]).InfoBo = new Rectangle(((Item)base.items[i]).Bounds.X + 0x4b, ((Item)base.items[i]).Bounds.Y + 0x16, ((int)graphics.MeasureString(((Item)base.items[i]).Info, this.Font).Width) + 2, (int)graphics.MeasureString(((Item)base.items[i]).Info, this.Font).Height);
            }
            base.OnCreateControl();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            foreach (Item item in Items)
            {
                if (item.ImageBo.Contains(base.m_ptMousePos))
                {
                    item.ImageMouseHover = true;
                    base.ClearItemMouseOn();
                    break;
                }
                if (item.InfoBo.Contains(base.m_ptMousePos))
                {
                    item.InfoHover = true;
                    base.ClearItemMouseOn();
                    break;
                }
                item.InfoHover = false;
                item.ImageMouseHover = false;
                this.Cursor = Cursors.Arrow;
                base.ClearItemMouseOn();
            }
            base.OnMouseMove(e);
        }

        [Editor(typeof(CommandCollectionEditor), typeof(UITypeEditor))]
        public override MyControlItemCollection Items
        {
            get
            {
                if (base.items == null)
                {
                    base.items = new MyControlItemCollection(this);
                }
                return base.items;
            }
        }
    }
}
