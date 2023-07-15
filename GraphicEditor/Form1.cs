namespace GraphicEditor
{
    public partial class Form1 : Form
    {
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


        DrawPen pen;

        List<PointF> points;
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            UpdateStyles();

            points = new();
            pen = new DrawPen(Color.Black, 20);
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
            zoomChange = e.Delta / 120;
            if (Math.Abs(zoomPow) >= 5 && Math.Abs(zoomPow + zoomChange) >= 5) return;
            zoomPow += zoomChange;
            zoomPow = Math.Max(zoomPow, -5);
            zoomPow = Math.Min(zoomPow, 5);
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
            //If move button clicked
            if (e.Button == MouseButtons.Middle)
            {
                //mouse down is true
                translate = true;
                //starting coordinates for move

                pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MoveImage);
            }
            else if (e.Button == MouseButtons.Left)
            {
                float locationX = mouseX / (ratio * zoomFac) - translateX;
                float locationY = mouseY / (ratio * zoomFac) - translateY;
                points.Add(new PointF(locationX, locationY));
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
            if (MathF.Abs(locationX - points.Last().X) > 0.676f || MathF.Abs(locationY - points.Last().Y) > 0.676f)
            {
                points.Add(new PointF(locationX, locationY));
                pictureBox1.Refresh();
            }
        }
        protected void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //set mouse down operation end
            if (e.Button == MouseButtons.Middle)
            {
                translate = false;
                pictureBox1.MouseMove -= new MouseEventHandler(pictureBox1_MoveImage);
            }
            if (e.Button == MouseButtons.Left)
            {
                draw = false;
                pictureBox1.MouseMove -= new MouseEventHandler(pictureBox1_DrawImage);
                ApplyLine();
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
                if (points.Count > 1)
                    for (int i = 1; i < points.Count; i++)
                        g.DrawLine(pen.show_pen, points[i - 1], points[i]);
                else g.FillEllipse(pen.brush, points[0].X - pen.ShowSize / 2, points[0].Y - pen.ShowSize / 2, pen.ShowSize, pen.ShowSize);
            }
        }
        private void ApplyLine()
        {
            PointF[] pts = points.ToArray();
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X = MathF.Round(pts[i].X / CoordTransform);
                pts[i].Y = MathF.Round(pts[i].Y / CoordTransform);
            }
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                if (points.Count > 1)
                {
                    for (int i = 1; i < pts.Length; i++)
                        gfx.DrawLine(pen.draw_pen, pts[i - 1], pts[i]);
                }
                else gfx.FillEllipse(pen.brush, pts[0].X - pen.Size / 2, pts[0].Y - pen.Size / 2, pen.Size, pen.Size);
            }
            points.Clear();
        }
    }
}