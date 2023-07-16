using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraphicEditor
{
    public class DrawPen
    {
        private Color color;
        public Color Color
        {
            get { return color; }
            set
            {
                color = draw_pen.Color = show_pen.Color = value;
                brush = new SolidBrush(value);
            }
        }

        private int size;
        public int Size
        {
            get { return size; }
            set
            {
                size = value;
                ShowSize = value * MainForm.CoordTransformX;
                draw_pen.Width = value;
            }
        }

        private float show_size;
        [JsonIgnore]
        public float ShowSize
        {
            get { return show_size; }
            private set
            {
                show_size = value;
                show_pen.Width = value;
            }
        }

        [JsonIgnore]
        public Pen show_pen { get; private set; }

        [JsonIgnore]
        public Pen draw_pen { get; private set; }

        [JsonIgnore]
        public Brush brush { get; private set; }

        public DrawPen(Color c, int size)
        {
            show_pen = new(Color.Empty) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round, MiterLimit = 0f };
            draw_pen = new(Color.Empty) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round, MiterLimit = 0f };
            Color = c;
            Size = size;
            brush = new SolidBrush(Color);
        }

        public void ToRoundBrush()
        {
            draw_pen.StartCap = show_pen.StartCap = LineCap.Round;
            draw_pen.EndCap = show_pen.EndCap = LineCap.Round;
            draw_pen.LineJoin = show_pen.LineJoin = LineJoin.Round;
            draw_pen.MiterLimit = show_pen.MiterLimit = 0f;
        }

        public void ToSquareBrush()
        {
            draw_pen.StartCap = show_pen.StartCap = LineCap.Flat;
            draw_pen.EndCap = show_pen.EndCap = LineCap.Flat;
            draw_pen.LineJoin = show_pen.LineJoin = LineJoin.Miter;
            draw_pen.MiterLimit = show_pen.MiterLimit = 10f;
        }

        public DrawPen Clone()
        {
            return new DrawPen(color, size);
        }
    }
}
