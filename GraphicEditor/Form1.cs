namespace GraphicEditor
{
    public partial class Form1 : Form
    {
        private enum Tools
        {
            Hand = 0, Brush, Line, Rectangle, Ellipse
        }

        private Tools selected;
        private Tools Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (value == Tools.Rectangle || value == Tools.Line) pen = new DrawPen(SelectedColor, Convert.ToInt32(numericUpDown1.Value), false);
                else pen = new DrawPen(SelectedColor, Convert.ToInt32(numericUpDown1.Value));
            }
        }

        private Tools oldMode;

        // Factor for zoom the image
        private float zoomFac = 1;
        private int zoomPow = 0;
        private int zoomChange = 0;

        public const float CoordTransform = 1.352f;
        //set Zoom allowed
        private bool zoomSet = false;

        //value for moving the image in X direction
        private float translateX = 0;
        //value for moving the image in Y direction
        private float translateY = 0;

        //Flag to set the moving operation set
        private bool translateSet = false;
        //Flag to set mouse down on the image
        private bool translate = false;

        private bool draw = false;

        //set on the mouse down to know from where moving starts
        private float mouseX;
        private float mouseY;


        //temporary storage in bitmap
        Image bmp;

        float ratio;
        float translateRatio;

        MouseButtons activatedBy = MouseButtons.None;

        DrawPen pen;
        BasicTool current;


        Bitmap palette;
        Point ColorLocation = new Point(225, 0);
        Color SelectedColor;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_MouseUp);

            pictureBox1.BorderStyle = BorderStyle.FixedSingle;

            pictureBox1.MouseWheel += new MouseEventHandler(Zoom);

            //Store the image in to bytes array
            //It is not advisable to store bitmap as such

            //Change the path name for your file
            if (System.IO.File.Exists(Application.StartupPath + "//test.jpg") != true)
            {
                MessageBox.Show("Your Image file does not exists");
                return;
            }

            if (bmp != null)
            {
                bmp.Dispose();
            }
            bmp = new Bitmap(Application.StartupPath + "//test.jpg");

            // Check potrait or landscape
            if (bmp.Width > bmp.Height)
            {
                ratio = (float)pictureBox1.Width / (float)bmp.Width;
                translateRatio = (float)bmp.Width / (float)pictureBox1.Width;

            }
            else
            {
                ratio = (float)pictureBox1.Height / (float)bmp.Height;
                translateRatio = (float)bmp.Height / (float)pictureBox1.Height;

            }

            this.Shown += new EventHandler(Form1_Shown);
            this.Disposed += new EventHandler(Form1_Disposed);

            Selected = Tools.Hand;

            palette = (Bitmap)pictureBox2.Image;
            pictureBox2.Refresh();
            SelectColor();
        }
        protected void Form1_Disposed(object? sender, EventArgs e)
        {
            //Dispose the bmp when form is disposed.
            if (bmp != null)
            {
                bmp.Dispose();
            }
        }

        protected void Form1_Shown(object? sender, EventArgs e)
        {
            //Draw the image initially
            pictureBox1.Refresh();

        }

        private void Zoom(object? sender, MouseEventArgs e)
        {
            if (translate || draw) return;

            zoomChange = e.Delta / 120;
            if ((zoomPow >= 10 || zoomPow <= 0) && (zoomPow + zoomChange >= 10 || zoomPow + zoomChange <= 0)) return;
            zoomPow += zoomChange;
            zoomPow = Math.Max(zoomPow, 0);
            zoomPow = Math.Min(zoomPow, 10);
            zoomFac = MathF.Pow(1.5f, zoomPow);
            //set Zoom allowed
            zoomSet = true;

            //call the picture box paint
            mouseX = e.X;
            mouseY = e.Y;
            pictureBox1.Refresh();

            //moving operation unset
            //translateSet = false;

        }

        protected void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            if (e.Button == MouseButtons.Middle && !draw)
            {
                oldMode = Selected;
                Selected = Tools.Hand;
                hand_btn.Select();
                activatedBy = e.Button;

                if (!translate)
                {
                    translate = true;
                    pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MoveImage);
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                activatedBy = e.Button;
                float locationX = mouseX / (ratio * zoomFac) - translateX;
                float locationY = mouseY / (ratio * zoomFac) - translateY;
                switch (Selected)
                {
                    case Tools.Hand:
                        if (!translate)
                        {
                            translate = true;
                            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MoveImage);
                        }
                        return;
                    case Tools.Brush:
                        current = new BrushTool(new PointF(locationX, locationY), pen);
                        break;
                    case Tools.Line:
                        current = new LineTool(new PointF(locationX, locationY), pen);
                        break;
                    case Tools.Rectangle:
                        current = new RectangleTool(new PointF(locationX, locationY), pen);
                        break;
                    case Tools.Ellipse:
                        current = new EllipseTool(new PointF(locationX, locationY), pen);
                        break;
                    default:
                        break;
                }
                draw = true;
                pictureBox1.Refresh();
                pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_DrawImage);
            }
        }

        protected void pictureBox1_MoveImage(object? sender, MouseEventArgs e)
        {
            //calculate the total distance to move from 0,0
            //previous image position+ current moving distance
            translateX += ((e.X - mouseX) * (translateRatio / zoomFac));
            translateY += ((e.Y - mouseY) * (translateRatio / zoomFac));

            pictureBox1.Refresh();

            mouseX = e.X;
            mouseY = e.Y;
            //set present position of the image after move.
        }

        protected void pictureBox1_DrawImage(object? sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            float locationX = mouseX / (ratio * zoomFac) - translateX;
            float locationY = mouseY / (ratio * zoomFac) - translateY;
            if (current.Update(locationX, locationY))
                pictureBox1.Refresh();
        }

        protected void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != activatedBy) return;
            if (translate)
            {
                translate = false;
                pictureBox1.MouseMove -= new MouseEventHandler(pictureBox1_MoveImage);
                if (e.Button == MouseButtons.Middle)
                {
                    Selected = oldMode;
                    switch (Selected)
                    {
                        case Tools.Brush:
                            brush_btn.Select();
                            break;
                        case Tools.Line:
                            line_btn.Select();
                            break;
                        case Tools.Rectangle:
                            rectangle_btn.Select();
                            break;
                        case Tools.Ellipse:
                            ellipse_btn.Select();
                            break;
                        default:
                            break;
                    }
                }
            }
            if (draw)
            {
                draw = false;
                pictureBox1.MouseMove -= new MouseEventHandler(pictureBox1_DrawImage);
                using (Graphics gfx = Graphics.FromImage(bmp)) current.DrawToImage(gfx);
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //Conditions to avoid to proceed further.
            if (bmp == null) { return; }


            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            //Scale transform operation on the picture box device context
            //zoomFac is global variable which can be used to get desired zoom
            g.ScaleTransform(ratio * zoomFac, ratio * zoomFac);

            if (zoomSet)
            {
                float tmp = MathF.Pow(1.5f, zoomChange);
                translateX += mouseX * (1 - tmp) * (translateRatio / zoomFac);
                translateY += mouseY * (1 - tmp) * (translateRatio / zoomFac);

                zoomSet = false;
            }

            g.TranslateTransform(translateX, translateY);

            g.DrawImage(bmp, 0, 0);

            if (draw)
            {
                current.DrawToScreen(g);
            }
        }

        private void hand_btn_click(object sender, EventArgs e)
        {
            Selected = Tools.Hand;
        }

        private void brush_btn_Click(object sender, EventArgs e)
        {
            Selected = Tools.Brush;
        }

        private void line_btn_Click(object sender, EventArgs e)
        {
            Selected = Tools.Line;
        }

        private void rectangle_btn_Click(object sender, EventArgs e)
        {
            Selected = Tools.Rectangle;
        }

        private void ellipse_btn_Click(object sender, EventArgs e)
        {
            Selected = Tools.Ellipse;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.MouseMove += new MouseEventHandler(palette_MouseMove);
        }

        private void palette_MouseMove(object? sender, MouseEventArgs e)
        {
            ColorLocation.X = Math.Max(Math.Min(e.X, palette.Width - 1), 0);
            ColorLocation.Y = Math.Max(Math.Min(e.Y, palette.Height - 1), 0);
            SelectColor();
            pictureBox2.Refresh();
        }

        private void SelectColor()
        {
            SelectedColor = palette.GetPixel(ColorLocation.X, ColorLocation.Y);
            panel3.BackColor = SelectedColor;
            label1.Text = "R: " + SelectedColor.R.ToString();
            label2.Text = "G: " + SelectedColor.G.ToString();
            label3.Text = "B: " + SelectedColor.B.ToString();
            pen.Color = SelectedColor;
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(new Pen(Color.DarkGray, 1), ColorLocation.X - 5, ColorLocation.Y - 5, 10, 10);
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.MouseMove -= new MouseEventHandler(palette_MouseMove);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pen.Size = Convert.ToInt32(numericUpDown1.Value);
            switch (Selected)
            {
                case Tools.Brush:
                    brush_btn.Select();
                    break;
                case Tools.Line:
                    line_btn.Select();
                    break;
                case Tools.Rectangle:
                    rectangle_btn.Select();
                    break;
                case Tools.Ellipse:
                    ellipse_btn.Select();
                    break;
                default:
                    break;
            }
        }
    }
}