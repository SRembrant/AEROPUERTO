using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Aeropuerto
{
    public class DotNetBarTabControl : TabControl
    {
        public DotNetBarTabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(44, 136);
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Alignment = TabAlignment.Left;
        }

        private Pen ToPen(Color color) => new Pen(color);
        private Brush ToBrush(Color color) => new SolidBrush(color);

        protected override void OnPaint(PaintEventArgs e)
        {
            using (Bitmap B = new Bitmap(Width, Height))
            using (Graphics G = Graphics.FromImage(B))
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(246, 248, 252)))
            using (Pen borderPen = new Pen(Color.FromArgb(170, 187, 204)))
            {
                try { SelectedTab.BackColor = Color.White; } catch { }

                G.Clear(Color.White);
                G.FillRectangle(bgBrush, new Rectangle(0, 0, ItemSize.Height + 4, Height));
                G.DrawLine(borderPen, new Point(ItemSize.Height + 3, 0), new Point(ItemSize.Height + 3, Height));

                for (int i = 0; i < TabCount; i++)
                {
                    Rectangle tabRect = GetTabRect(i);
                    Rectangle x2 = new Rectangle(new Point(tabRect.X - 2, tabRect.Y - 2),
                        new Size(tabRect.Width + 3, (i == SelectedIndex) ? tabRect.Height - 1 : tabRect.Height + 1));

                    if (i == SelectedIndex)
                    {
                        ColorBlend blend = new ColorBlend
                        {
                            Colors = new[] {
                                Color.FromArgb(232, 232, 240),
                                Color.FromArgb(232, 232, 240),
                                Color.FromArgb(232, 232, 240)
                            },
                            Positions = new[] { 0.0f, 0.5f, 1.0f }
                        };

                        using (LinearGradientBrush lgBrush = new LinearGradientBrush(x2, Color.Black, Color.Black, 90.0f))
                        {
                            lgBrush.InterpolationColors = blend;
                            G.FillRectangle(lgBrush, x2);
                        }

                        G.DrawRectangle(borderPen, x2);

                        G.SmoothingMode = SmoothingMode.HighQuality;
                        Point[] p = {
                            new Point(ItemSize.Height - 3, tabRect.Y + 20),
                            new Point(ItemSize.Height + 4, tabRect.Y + 14),
                            new Point(ItemSize.Height + 4, tabRect.Y + 27)
                        };

                        using (Pen whitePen = new Pen(Color.FromArgb(170, 187, 204)))
                        {
                            G.FillPolygon(Brushes.White, p);
                            G.DrawPolygon(whitePen, p);
                        }
                    }
                    else
                    {
                        G.FillRectangle(bgBrush, x2);
                        G.DrawLine(borderPen, new Point(x2.Right, x2.Top), new Point(x2.Right, x2.Bottom));
                    }

                    // Texto e imagen
                    StringFormat format = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    };

                    try
                    {
                        if (ImageList != null && ImageList.Images.IsValidIndex(TabPages[i].ImageIndex))
                        {
                            var image = ImageList.Images[TabPages[i].ImageIndex];
                            if (image != null)
                            {
                                G.DrawImage(image, new Point(x2.X + 8, x2.Y + 6));
                                G.DrawString("      " + TabPages[i].Text,
                                    (i == SelectedIndex) ? new Font(Font.FontFamily, Font.Size, FontStyle.Bold) : Font,
                                    Brushes.DimGray, x2, format);
                                continue;
                            }
                        }
                    }
                    catch { }

                    G.DrawString(TabPages[i].Text,
                        (i == SelectedIndex) ? new Font(Font.FontFamily, Font.Size, FontStyle.Bold) : Font,
                        Brushes.DimGray, x2, format);
                }

                e.Graphics.DrawImage(B, 0, 0);
            }
        }
    }

    public static class Extensions
    {
        public static bool IsValidIndex(this ImageList.ImageCollection imgList, int index)
        {
            return index >= 0 && index < imgList.Count;
        }
    }
}
