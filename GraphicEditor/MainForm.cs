using System.Drawing.Imaging;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace GraphicEditor
{
    public partial class MainForm : Form
    {
        private enum Tools
        {
            Hand = 0, Brush, Line, Rectangle, Ellipse, Eraser
        }

        Bitmap[] icons = { Properties.Resources.hand, Properties.Resources.brush, Properties.Resources.line, Properties.Resources.rectangle, Properties.Resources.ellipse, Properties.Resources.eraser };

        private Tools selected;
        private Tools Selected
        {
            get { return selected; }
            set
            {
                if (current is IEditable && edit) ApplyChanges();

                selected = value;
                toolStripStatusToolIcon.Image = icons[(int)value];
                switch (value)
                {
                    case Tools.Hand:
                        toolStripStatusToolName.Text = "Ðóêà";
                        current = null;
                        break;
                    case Tools.Brush:
                        toolStripStatusToolName.Text = "Êèñòü";
                        current.Pen.Color = SelectedColor;
                        current.Pen.ToRoundBrush();
                        current = new BrushTool(current.Pen);
                        break;
                    case Tools.Line:
                        toolStripStatusToolName.Text = "Ëèíèÿ";
                        current.Pen.Color = SelectedColor;
                        current.Pen.ToSquareBrush();
                        current = new LineTool(current.Pen);
                        break;
                    case Tools.Rectangle:
                        toolStripStatusToolName.Text = "Ïðÿìîóãîëüíèê";
                        current.Pen.Color = SelectedColor;
                        current.Pen.ToSquareBrush();
                        current = new RectangleTool(current.Pen);
                        break;
                    case Tools.Ellipse:
                        toolStripStatusToolName.Text = "Ýëëèïñ";
                        current.Pen.Color = SelectedColor;
                        current.Pen.ToRoundBrush();
                        current = new EllipseTool(current.Pen);
                        break;
                    case Tools.Eraser:
                        toolStripStatusToolName.Text = "Ëàñòèê";
                        current.Pen.Color = Color.White;
                        current.Pen.ToRoundBrush();
                        current = new BrushTool(current.Pen);
                        break;
                    default:
                        break;
                }

                if (current is IEditable) EditPanel.Visible = true;
                else EditPanel.Visible = false;

                apply_btn.Enabled = false;
            }
        }

        private string fileName;
        private string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                Text = "GraphicsEditor - " + value;
                Text += (saved) ? "" : "*";
            }
        }

        private string? filePath;

        private float zoomFac = 1;
        private int zoomPow = 0;
        private int zoomChange = 0;

        public static float CoordTransformX;
        public static float CoordTransformY;

        private bool zoomSet = false;

        private float translateX = 0;
        private float translateY = 0;

        private bool translate = false;
        private bool draw = false;
        private bool edit = false;
        private bool Edit
        {
            get { return edit; }
            set
            {
                apply_btn.Enabled = edit = value;
            }
        }

        private float mouseX;
        private float mouseY;

        Image bmp, original;

        float ratio;
        float translateRatio;

        MouseButtons activatedBy = MouseButtons.None;

        History history = new History();
        BasicTool current;
        bool saved = false;


        Bitmap palette;
        Point ColorLocation = new Point(225, 0);
        Color SelectedColor;

        public MainForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            UpdateStyles();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileName = "Áåçûìÿííûé";
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_MouseUp);

            pictureBox1.BorderStyle = BorderStyle.FixedSingle;

            pictureBox1.MouseWheel += new MouseEventHandler(Zoom);

            if (bmp != null)
            {
                bmp.Dispose();
            }
            bmp = new Bitmap(1080, 1920);
            PictureInit();
            using (Graphics g = Graphics.FromImage(bmp)) g.Clear(Color.White);
            original = new Bitmap(bmp);

            this.Shown += new EventHandler(Form1_Shown);
            this.Disposed += new EventHandler(Form1_Disposed);

            Selected = Tools.Hand;
            current = new BrushTool(new DrawPen(Color.Black, 1));

            palette = (Bitmap)pictureBox2.Image;
            pictureBox2.Refresh();
            SelectColor();
        }

        protected void PictureInit()
        {
            translate = false;
            draw = false;
            zoomPow = zoomChange = 0;
            zoomFac = 1;

            CoordTransformX = pictureBox1.CreateGraphics().DpiX / bmp.HorizontalResolution;
            CoordTransformY = pictureBox1.CreateGraphics().DpiY / bmp.VerticalResolution;

            // Check potrait or landscape
            if (bmp.Width > bmp.Height)
            {
                ratio = (float)pictureBox1.Width / (float)bmp.Width / CoordTransformX;
                translateRatio = (float)bmp.Width / (float)pictureBox1.Width * CoordTransformX;
                translateX = 0;
                translateY = (pictureBox1.Height * translateRatio - bmp.Height) / 2f;
            }
            else
            {
                ratio = (float)pictureBox1.Height / (float)bmp.Height / CoordTransformY;
                translateRatio = (float)bmp.Height / (float)pictureBox1.Height * CoordTransformY;
                translateX = (pictureBox1.Width * translateRatio - bmp.Width) / 2f;
                translateY = 0;
            }
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
            if ((zoomPow >= 9 || zoomPow <= -1) && (zoomPow + zoomChange >= 9 || zoomPow + zoomChange <= -1)) return;
            zoomPow += zoomChange;
            zoomPow = Math.Max(zoomPow, -1);
            zoomPow = Math.Min(zoomPow, 9);
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
            current.Pen.Size = Convert.ToInt32(numericUpDown1.Value);
            mouseX = e.X;
            mouseY = e.Y;
            if (e.Button == MouseButtons.Middle && !draw)
            {
                activatedBy = e.Button;

                if (!translate)
                {
                    translate = true;
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (current is IEditable && edit) ApplyChanges();

                activatedBy = e.Button;
                float locationX = mouseX / (ratio * zoomFac) - translateX;
                float locationY = mouseY / (ratio * zoomFac) - translateY;
                switch (Selected)
                {
                    case Tools.Hand:
                        hand_btn.Select();
                        if (!translate)
                        {
                            translate = true;
                        }
                        return;
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
                    case Tools.Eraser:
                        eraser_btn.Select();
                        break;
                    default:
                        break;
                }
                current.Initialize(locationX, locationY);
                draw = true;
                pictureBox1.Refresh();
            }
        }

        protected void pictureBox1_MouseMove(object? sender, MouseEventArgs e)
        {
            if (translate)
            {
                translateX += ((e.X - mouseX) * (translateRatio / zoomFac));
                translateY += ((e.Y - mouseY) * (translateRatio / zoomFac));

                pictureBox1.Refresh();
            }

            mouseX = e.X;
            mouseY = e.Y;
            float locationX = mouseX / (ratio * zoomFac) - translateX;
            float locationY = mouseY / (ratio * zoomFac) - translateY;
            toolStripStatusCoords.Text = MathF.Floor(locationX / CoordTransformX).ToString() + ", " + MathF.Floor(locationY / CoordTransformY).ToString();
            if (draw && current.Update(locationX, locationY))
                pictureBox1.Refresh();
        }

        protected void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != activatedBy) return;
            if (translate)
            {
                translate = false;
            }
            else if (draw)
            {
                draw = false;
                if (current is IEditable)
                {
                    Edit = true;
                    îòìåíèòüToolStripMenuItem.Enabled = true;
                    saved = false;
                    FileName += "";
                }
                else ApplyChanges();
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

            if (draw || edit)
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

        private void eraser_btn_Click(object sender, EventArgs e)
        {
            Selected = Tools.Eraser;
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
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void SelectColor()
        {
            SelectedColor = palette.GetPixel(ColorLocation.X, ColorLocation.Y);
            panel3.BackColor = SelectedColor;
            label1.Text = "R: " + SelectedColor.R.ToString();
            label2.Text = "G: " + SelectedColor.G.ToString();
            label3.Text = "B: " + SelectedColor.B.ToString();
            current.Pen.Color = SelectedColor;
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(new Pen(Color.DarkGray, 1), ColorLocation.X - 5, ColorLocation.Y - 5, 10, 10);
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.MouseMove -= new MouseEventHandler(palette_MouseMove);
            ColorLocation.X = Math.Max(Math.Min(e.X, palette.Width - 1), 0);
            ColorLocation.Y = Math.Max(Math.Min(e.Y, palette.Height - 1), 0);
            SelectColor();
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            current.Pen.Size = Convert.ToInt32(numericUpDown1.Value);
            ReturnSelection();
            pictureBox1.Refresh();
        }

        private void ñîçäàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FileCreateForm f_cr = new FileCreateForm())
            {
                var result = f_cr.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (!saved)
                        if (NotSavedAlert() == DialogResult.Cancel)
                            return;

                    bmp.Dispose();
                    bmp = new Bitmap(f_cr.Width, f_cr.Height);
                    PictureInit();
                    using (Graphics g = Graphics.FromImage(bmp)) g.Clear(Color.White);
                    original = new Bitmap(bmp);
                    FileName = "Áåçûìÿííûé";
                    pictureBox1.Refresh();
                }
                history.Clear();
            }
        }

        private void îòêðûòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new() { Multiselect = false, InitialDirectory = Application.StartupPath, Filter = "Âñå òèïû èçîáðàæåíèé (*.bmp, *.gif, *.jpg, *.png, *.tiff)|*.bmp;*.gif;*.jpg;*.png;*.tiff|BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif|JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|TIFF (*.tiff)|*.tiff" })
            {
                var result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (!saved)
                        if (NotSavedAlert() == DialogResult.Cancel)
                        {
                            this.Show();
                            return;
                        }

                    bmp.Dispose();
                    bmp = new Bitmap(ofd.FileName);
                    PictureInit();
                    original = new Bitmap(bmp);
                    filePath = ofd.FileName;
                    saved = true;
                    FileName = Path.GetFileName(filePath);
                    pictureBox1.Refresh();
                }
                history.Clear();
            }
        }

        private void ñîõðàíèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath is not null) SaveFile();
            else SaveAs();
        }

        private void ñîõðàíèòüÊàêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void SaveAs()
        {
            using (SaveFileDialog sfd = new() { InitialDirectory = Application.StartupPath, Filter = "BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif|JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|TIFF (*.tiff)|*.tiff" })
            {
                var result = sfd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    filePath = sfd.FileName;
                    SaveFile();
                }
            }
        }

        private void SaveFile()
        {
            string extension = Path.GetExtension(filePath).ToLower();
            ImageFormat imf = ImageFormat.Bmp;
            if (extension == ".gif") imf = ImageFormat.Gif;
            else if (extension == ".jpg") imf = ImageFormat.Jpeg;
            else if (extension == ".png") imf = ImageFormat.Png;
            else if (extension == ".tiff") imf = ImageFormat.Tiff;

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    bmp.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            saved = true;
            FileName = Path.GetFileName(filePath);
        }

        private void îòìåíèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edit)
            {
                Edit = false;
                numericUpDown2.Value = 0;
            }
            else
            {
                saved = false;
                FileName += "";
                history.Remove();
                bmp.Dispose();
                bmp = new Bitmap(original);
                using (Graphics g = Graphics.FromImage(bmp)) history.Save(g);
            }
            if (history.Length == 0) îòìåíèòüToolStripMenuItem.Enabled = false;
            pictureBox1.Refresh();
        }

        DialogResult NotSavedAlert()
        {
            return MessageBox.Show(
                "Òåêóùèé ôàéë íå ñîõðàí¸í!",
                "Âíèìàíèå!",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RightAlign
                );
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
                if (NotSavedAlert() == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    this.BringToFront();
                }
        }

        private void apply_btn_Click(object sender, EventArgs e)
        {
            ApplyChanges();
            ReturnSelection();
            pictureBox1.Refresh();
        }

        private void ApplyChanges()
        {
            using (Graphics gfx = Graphics.FromImage(bmp)) current.DrawToImage(gfx);
            history.Add(current.Clone());
            saved = false;
            FileName += "";
            îòìåíèòüToolStripMenuItem.Enabled = true;
            Edit = false;
            numericUpDown2.Value = 0;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Value %= 360;
            (current as IEditable).Rotation = Convert.ToSingle(numericUpDown2.Value);
            ReturnSelection();
            pictureBox1.Refresh();
        }

        private void ReturnSelection()
        {
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
                case Tools.Eraser:
                    eraser_btn.Select();
                    break;
                default:
                    break;
            }
        }
    }
}