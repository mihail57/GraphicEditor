using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicEditor
{
    public abstract class BasicTool
    {
        public DrawPen Pen { get; set; }

        public void DrawToScreen(Graphics g)
        {
            Draw(g, Pen.show_pen, 1f);
        }
        public void DrawToImage(Graphics g)
        {
            Draw(g, Pen.draw_pen, Form1.CoordTransform);
        }

        protected abstract void Draw(Graphics g, Pen p, float c);

        public abstract bool Update(float x, float y);

        protected BasicTool(DrawPen pen)
        {
            Pen = pen;
        }
    }

    public class BrushTool : BasicTool
    {
        protected List<PointF> Points;

        public BrushTool(PointF location, DrawPen pen)
        : base(pen)
        {
            Points = new List<PointF>()
            {
                location
            };
        }

        protected override void Draw(Graphics g, Pen p, float c)
        {
            PointF[] pts = Points.ToArray();
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X = MathF.Round(pts[i].X / c);
                pts[i].Y = MathF.Round(pts[i].Y / c);
            }
            if (Points.Count > 1)
            {
                g.DrawLines(p, pts);
            }
            else g.FillEllipse(Pen.brush, pts[0].X - p.Width / 2, pts[0].Y - p.Width / 2, p.Width, p.Width);
        }

        public override bool Update(float x, float y)
        {
            if (MathF.Abs(x - Points.Last().X) > Form1.CoordTransform || MathF.Abs(x - Points.Last().Y) > Form1.CoordTransform)
            {
                Points.Add(new PointF(x, y));
                return true;
            }
            return false;

        }
    }

    public class LineTool : BasicTool
    {
        private PointF start;
        private PointF end;

        public LineTool(PointF s, DrawPen pen)
        : base(pen)
        {
            start = end = s;
        }

        public override bool Update(float x, float y)
        {
            if (MathF.Abs(x - end.X) > Form1.CoordTransform || MathF.Abs(y - end.Y) > Form1.CoordTransform)
            {
                end.X = x;
                end.Y = y;
                return true;
            }
            return false;
        }

        protected override void Draw(Graphics g, Pen p, float c)
        {
            g.DrawLine(p, start.X / c, start.Y / c, end.X / c, end.Y / c);
        }
    }

    public abstract class BasicRectangularTool : BasicTool
    {
        protected PointF Location;
        protected float Width;
        protected float Height;

        protected BasicRectangularTool(PointF location, DrawPen pen)
        : base(pen)
        {
            Location = location;
            Width = Height = 0f;
        }

        public override bool Update(float newX, float newY)
        {
            newX -= Location.X;
            newY -= Location.Y;
            if (MathF.Abs(Width - newX) > Form1.CoordTransform || MathF.Abs(Height - newY) > Form1.CoordTransform)
            {
                Width = newX;
                Height = newY;

                return true;
            }
            return false;
        }
    }

    public class RectangleTool : BasicRectangularTool
    {
        public RectangleTool(PointF location, DrawPen pen) : base(location, pen) { }

        protected override void Draw(Graphics g, Pen p, float c)
        {
            float locX = Location.X, locY = Location.Y, w = Width, h = Height;
            if(w < 0)
            {
                locX += w;
                w *= -1;
            }
            if (h < 0)
            {
                locY += h;
                h *= -1;
            }

            if (MathF.Floor(w / c) < 1.5f * p.Width && MathF.Floor(h / c) == 1.5f * p.Width)
            {
                g.DrawRectangle(p, locX / c, locY / c, 1, 1);
            }
            else if (MathF.Floor(w / c) == 0) g.DrawLine(p, locX / c, locY / c - p.Width / 2, locX / c, locY / c + h + p.Width / 2);
            else if (MathF.Floor(h / c) == 0) g.DrawLine(p, locX / c - p.Width / 2, locY / c, locX / c + w + p.Width / 2, locY / c);
            else g.DrawRectangle(p, locX / c, locY / c, w / c, h / c);
        }
    }
    public class EllipseTool : BasicRectangularTool
    {
        public EllipseTool(PointF location, DrawPen pen) : base(location, pen) { }

        protected override void Draw(Graphics g, Pen p, float c)
        {
            if (MathF.Floor(Width / c) == 0 && MathF.Floor(Height / c) == 0) g.DrawEllipse(p, Location.X / c, Location.Y / c, 1, 1);
            else if (MathF.Floor(Width / c) == 0) g.DrawLine(p, Location.X / c, Location.Y / c, Location.X / c, Location.Y / c + Height);
            else if (MathF.Floor(Height / c) == 0) g.DrawLine(p, Location.X / c, Location.Y / c, Location.X / c + Width, Location.Y / c);
            else g.DrawEllipse(p, Location.X / c, Location.Y / c, Width / c, Height / c);
        }
    }
}
