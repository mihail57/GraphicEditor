using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

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

        public abstract void Initialize(float x, float y);

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

        public BrushTool(DrawPen pen)
        : base(pen)
        {
            Points = new List<PointF>();
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

        public override void Initialize(float x, float y) => Points.Add(new PointF(x, y));
    }

    public class LineTool : BasicTool, IEditable
    {
        private PointF start;
        private PointF end;

        public float Rotation { get; set; }

        public LineTool(DrawPen pen)
        : base(pen)
        {
            start = end = new PointF(0f, 0f);
        }

        protected LineTool(PointF start, PointF end, DrawPen pen, float rot)
        : base(pen)
        {
            this.start = start;
            this.end = end;
            Rotation = rot;
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
            using (GraphicsPath g_p = new GraphicsPath())
            {
                g_p.AddLine(MathF.Floor(start.X / cx), MathF.Floor(start.Y / cy), MathF.Floor(end.X / cx), MathF.Floor(end.Y / cy));
                (this as IEditable).ApplyRotation(g_p);
                g.DrawPath(p, g_p);
            }
        }

        public override BasicTool Clone()
        {
            DrawPen clone = Pen.Clone();
            clone.ToSquareBrush();
            return new LineTool(start, end, clone, Rotation);
        }

        public override void Initialize(float x, float y)
        {
            end.X = start.X = x;
            end.Y = start.Y = y;
        }

        void IEditable.ApplyRotation(GraphicsPath g)
        {
            float middleX = (start.X + end.X) / 2, middleY = (start.Y + end.Y) / 2;
            g.Transform((this as IEditable).GetRotationMatrix(middleX, middleY));
        }
    }

    public abstract class BasicRectangularTool : BasicTool, IEditable
    {
        protected PointF Location;
        protected float Width;
        protected float Height;

        public float Rotation { get; set; }

        protected BasicRectangularTool(DrawPen pen)
        : base(pen)
        {
            Location = new PointF(0, 0);
            Width = Height = 0f;
        }

        protected BasicRectangularTool(PointF location, float width, float height, DrawPen pen, float rot)
        : base(pen)
        {
            Location = location;
            Width = width;
            Height = height;
            Rotation = rot;
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

        public override void Initialize(float x, float y)
        {
            Location.X = x;
            Location.Y = y;
            Width = Height = 0f;
        }

        void IEditable.ApplyRotation(GraphicsPath g)
        {
            float middleX = Location.X + Width / 2, middleY = Location.Y + Height / 2;
            g.Transform((this as IEditable).GetRotationMatrix(middleX, middleY));
        }
    }

    public class RectangleTool : BasicRectangularTool
    {
        public RectangleTool(DrawPen pen) : base(pen) { }

        protected RectangleTool(PointF location, float width, float height, DrawPen pen, float rot) : base(location, width, height, pen, rot) { }

        protected override void Draw(Graphics g, Pen p, float cx, float cy)
        {
            using (GraphicsPath g_p = new GraphicsPath())
            {
                float locX = MathF.Round(Location.X), locY = MathF.Round(Location.Y), w = MathF.Round(Width), h = MathF.Round(Height);
                if (w < 0)
                {
                    locX += w;
                    w *= -1;
                }
                if (h < 0)
                {
                    locY += h;
                    h *= -1;
                }

                if (MathF.Floor(w / cx) == 0 && MathF.Floor(h / cy) == 0) g_p.AddRectangle(new RectangleF(MathF.Floor(locX / cx), MathF.Floor(locY / cy), 1, 1));
                else if (MathF.Floor(w / cx) == 0) g_p.AddLine(MathF.Floor(locX / cx), MathF.Floor(locY / cy - p.Width / 2), MathF.Floor(locX / cx), MathF.Floor((locY + h) / cy + p.Width / 2));
                else if (MathF.Floor(h / cy) == 0) g_p.AddLine(MathF.Floor(locX / cx - p.Width / 2), MathF.Floor(locY / cy), MathF.Floor((locX + w) / cx + p.Width / 2), MathF.Floor(locY / cy));
                else g_p.AddRectangle(new RectangleF(MathF.Floor(locX / cx), MathF.Floor(locY / cy), MathF.Floor(w / cx), MathF.Floor(h / cy)));

                (this as IEditable).ApplyRotation(g_p);
                g.DrawPath(p, g_p);
            }
        }

        public override BasicTool Clone()
        {
            DrawPen clone = Pen.Clone();
            clone.ToSquareBrush();
            return new RectangleTool(Location, Width, Height, clone, Rotation);
        }
    }
    public class EllipseTool : BasicRectangularTool
    {
        public EllipseTool(DrawPen pen) : base(pen) { }

        protected EllipseTool(PointF location, float width, float height, DrawPen pen, float rot) : base(location, width, height, pen, rot) { }

        protected override void Draw(Graphics g, Pen p, float cx, float cy)
        {
            using (GraphicsPath g_p = new GraphicsPath())
            {
                if (MathF.Floor(Width / cx) == 0 && MathF.Floor(Height / cy) == 0) g_p.AddEllipse(MathF.Floor(Location.X / cx), MathF.Floor(Location.Y / cy), 1, 1);
                else if (MathF.Floor(Width / cx) == 0) g_p.AddLine(MathF.Floor(Location.X / cx), MathF.Floor(Location.Y / cy), MathF.Floor(Location.X / cx), MathF.Floor((Location.Y + Height) / cy));
                else if (MathF.Floor(Height / cy) == 0) g_p.AddLine(MathF.Floor(Location.X / cx), MathF.Floor(Location.Y / cy), MathF.Floor((Location.X + Width) / cx), MathF.Floor(Location.Y / cy));
                else g_p.AddEllipse(MathF.Floor(Location.X / cx), MathF.Floor(Location.Y / cy), MathF.Floor(Width / cx), MathF.Floor(Height / cy));

                (this as IEditable).ApplyRotation(g_p);
                g.DrawPath(p, g_p);
            }
        }
        public override BasicTool Clone()
        {
            return new EllipseTool(Location, Width, Height, Pen.Clone(), Rotation);
        }
    }

    public interface IEditable
    {
        float Rotation { get; set; }

        void ApplyRotation(GraphicsPath g);

        Matrix GetRotationMatrix(float x, float y)
        {
            Matrix m = new Matrix();
            m.Translate(x, y);
            m.Rotate(Rotation);
            m.Translate(-x, -y);
            return m;
        }
    }
}
