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
            Draw(g, Pen.show_pen, 1f, 1f);
        }
        public void DrawToImage(Graphics g)
        {
            Draw(g, Pen.draw_pen, MainForm.CoordTransformX, MainForm.CoordTransformY);
        }

        protected abstract void Draw(Graphics g, Pen p, float cx, float cy);

        public abstract bool Update(float x, float y);

        public abstract BasicTool Clone();

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

        protected BrushTool(List<PointF> points, DrawPen pen)
        : base(pen)
        {
            Points = new List<PointF>(points);
        }

        protected override void Draw(Graphics g, Pen p, float cx, float cy)
        {
            PointF[] pts = Points.ToArray();
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X = MathF.Floor(pts[i].X / cx);
                pts[i].Y = MathF.Floor(pts[i].Y / cy);
            }
            if (Points.Count > 1)
            {
                g.DrawLines(p, pts);
            }
            else g.FillEllipse(Pen.brush, MathF.Floor(pts[0].X - p.Width / 2), MathF.Floor(pts[0].Y - p.Width / 2), p.Width, p.Width);
        }

        public override bool Update(float x, float y)
        {
            if (MathF.Abs(x - Points.Last().X) > MainForm.CoordTransformX || MathF.Abs(x - Points.Last().Y) > MainForm.CoordTransformY)
            {
                Points.Add(new PointF(x, y));
                return true;
            }
            return false;
        }

        public override BasicTool Clone()
        {
            return new BrushTool(Points, Pen.Clone());
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

        protected LineTool(PointF start, PointF end, DrawPen pen)
        : base(pen)
        {
            this.start = start;
            this.end = end;
        }

        public override bool Update(float x, float y)
        {
            if (MathF.Abs(x - end.X) > MainForm.CoordTransformX || MathF.Abs(y - end.Y) > MainForm.CoordTransformY)
            {
                end.X = x;
                end.Y = y;
                return true;
            }
            return false;
        }

        protected override void Draw(Graphics g, Pen p, float cx, float cy)
        {
            g.DrawLine(p, MathF.Floor(start.X / cx), MathF.Floor(start.Y / cy), MathF.Floor(end.X / cx), MathF.Floor(end.Y / cy));
        }

        public override BasicTool Clone()
        {
            DrawPen clone = Pen.Clone();
            clone.ToSquareBrush();
            return new LineTool(start, end, clone);
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

        protected BasicRectangularTool(PointF location, float width, float height, DrawPen pen)
        : base(pen)
        {
            Location = location;
            Width = width;
            Height = height;
        }

        public override bool Update(float newX, float newY)
        {
            newX -= Location.X;
            newY -= Location.Y;
            if (MathF.Abs(Width - newX) > MainForm.CoordTransformX || MathF.Abs(Height - newY) > MainForm.CoordTransformY)
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

        protected RectangleTool(PointF location, float width, float height, DrawPen pen) : base(location, width, height, pen) { }

        protected override void Draw(Graphics g, Pen p, float cx, float cy)
        {
            float locX = MathF.Round(Location.X), locY = MathF.Round(Location.Y), w = MathF.Round(Width), h = MathF.Round(Height);
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

            if (MathF.Floor(w / cx) == 0 && MathF.Floor(h / cy) == 0) g.DrawRectangle(p, MathF.Floor(locX / cx), MathF.Floor(locY / cy), 1, 1);
            else if (MathF.Floor(w / cx) == 0) g.DrawLine(p, MathF.Floor(locX / cx), MathF.Floor(locY / cy - p.Width / 2), MathF.Floor(locX / cx), MathF.Floor((locY + h) / cy + p.Width / 2));
            else if (MathF.Floor(h / cy) == 0) g.DrawLine(p, MathF.Floor(locX / cx - p.Width / 2), MathF.Floor(locY / cy), MathF.Floor((locX + w) / cx + p.Width / 2), MathF.Floor(locY / cy));
            else g.DrawRectangle(p, MathF.Floor(locX / cx), MathF.Floor(locY / cy), MathF.Floor(w / cx), MathF.Floor(h / cy));
        }

        public override BasicTool Clone()
        {
            DrawPen clone = Pen.Clone();
            clone.ToSquareBrush();
            return new RectangleTool(Location, Width, Height, clone);
        }
    }
    public class EllipseTool : BasicRectangularTool
    {
        public EllipseTool(PointF location, DrawPen pen) : base(location, pen) { }

        protected EllipseTool(PointF location, float width, float height, DrawPen pen) : base(location, width, height, pen) { }

        protected override void Draw(Graphics g, Pen p, float cx, float cy)
        {
            if (MathF.Floor(Width / cx) == 0 && MathF.Floor(Height / cy) == 0) g.DrawEllipse(p, MathF.Floor(Location.X / cx), MathF.Floor(Location.Y / cy), 1, 1);
            else if (MathF.Floor(Width / cx) == 0) g.DrawLine(p, MathF.Floor(Location.X / cx), MathF.Floor(Location.Y / cy), MathF.Floor(Location.X / cx), MathF.Floor((Location.Y + Height) / cy));
            else if (MathF.Floor(Height / cy) == 0) g.DrawLine(p, MathF.Floor(Location.X / cx), MathF.Floor(Location.Y / cy), MathF.Floor((Location.X + Width) / cx), MathF.Floor(Location.Y / cy));
            else g.DrawEllipse(p, MathF.Floor(Location.X / cx), MathF.Floor(Location.Y / cy), MathF.Floor(Width / cx), MathF.Floor(Height / cy));
        }
        public override BasicTool Clone()
        {
            return new EllipseTool(Location, Width, Height, Pen.Clone());
        }
    }
}
