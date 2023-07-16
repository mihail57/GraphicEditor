namespace GraphicEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            eraser_btn = new Button();
            ellipse_btn = new Button();
            rectangle_btn = new Button();
            line_btn = new Button();
            brush_btn = new Button();
            hand_btn = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusToolIcon = new ToolStripStatusLabel();
            toolStripStatusToolName = new ToolStripStatusLabel();
            toolStripStatusPlaceholder = new ToolStripStatusLabel();
            toolStripStatusCoords = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            создатьToolStripMenuItem = new ToolStripMenuItem();
            открытьToolStripMenuItem = new ToolStripMenuItem();
            сохранитьToolStripMenuItem = new ToolStripMenuItem();
            сохранитьКакToolStripMenuItem = new ToolStripMenuItem();
            правкаToolStripMenuItem = new ToolStripMenuItem();
            отменитьToolStripMenuItem = new ToolStripMenuItem();
            panel2 = new Panel();
            numericUpDown1 = new NumericUpDown();
            label4 = new Label();
            panel3 = new Panel();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            panel4 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(661, 495);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(eraser_btn);
            panel1.Controls.Add(ellipse_btn);
            panel1.Controls.Add(rectangle_btn);
            panel1.Controls.Add(line_btn);
            panel1.Controls.Add(brush_btn);
            panel1.Controls.Add(hand_btn);
            panel1.Location = new Point(12, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(38, 503);
            panel1.TabIndex = 1;
            // 
            // eraser_btn
            // 
            eraser_btn.Image = Properties.Resources.eraser;
            eraser_btn.Location = new Point(3, 183);
            eraser_btn.Name = "eraser_btn";
            eraser_btn.Size = new Size(30, 30);
            eraser_btn.TabIndex = 5;
            eraser_btn.UseVisualStyleBackColor = true;
            eraser_btn.Click += eraser_btn_Click;
            // 
            // ellipse_btn
            // 
            ellipse_btn.Image = Properties.Resources.ellipse;
            ellipse_btn.Location = new Point(3, 147);
            ellipse_btn.Name = "ellipse_btn";
            ellipse_btn.Size = new Size(30, 30);
            ellipse_btn.TabIndex = 4;
            ellipse_btn.UseVisualStyleBackColor = true;
            ellipse_btn.Click += ellipse_btn_Click;
            // 
            // rectangle_btn
            // 
            rectangle_btn.Image = Properties.Resources.rectangle;
            rectangle_btn.Location = new Point(3, 111);
            rectangle_btn.Name = "rectangle_btn";
            rectangle_btn.Size = new Size(30, 30);
            rectangle_btn.TabIndex = 3;
            rectangle_btn.UseVisualStyleBackColor = true;
            rectangle_btn.Click += rectangle_btn_Click;
            // 
            // line_btn
            // 
            line_btn.Image = Properties.Resources.line;
            line_btn.Location = new Point(3, 75);
            line_btn.Name = "line_btn";
            line_btn.Size = new Size(30, 30);
            line_btn.TabIndex = 2;
            line_btn.UseVisualStyleBackColor = true;
            line_btn.Click += line_btn_Click;
            // 
            // brush_btn
            // 
            brush_btn.Image = Properties.Resources.brush;
            brush_btn.Location = new Point(3, 39);
            brush_btn.Name = "brush_btn";
            brush_btn.Size = new Size(30, 30);
            brush_btn.TabIndex = 1;
            brush_btn.UseVisualStyleBackColor = true;
            brush_btn.Click += brush_btn_Click;
            // 
            // hand_btn
            // 
            hand_btn.Image = Properties.Resources.hand;
            hand_btn.Location = new Point(3, 3);
            hand_btn.Name = "hand_btn";
            hand_btn.Size = new Size(30, 30);
            hand_btn.TabIndex = 0;
            hand_btn.UseVisualStyleBackColor = true;
            hand_btn.Click += hand_btn_click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusToolIcon, toolStripStatusToolName, toolStripStatusPlaceholder, toolStripStatusCoords });
            statusStrip1.Location = new Point(0, 538);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(978, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusToolIcon
            // 
            toolStripStatusToolIcon.Image = Properties.Resources.hand;
            toolStripStatusToolIcon.Name = "toolStripStatusToolIcon";
            toolStripStatusToolIcon.Size = new Size(16, 17);
            // 
            // toolStripStatusToolName
            // 
            toolStripStatusToolName.Name = "toolStripStatusToolName";
            toolStripStatusToolName.Size = new Size(74, 17);
            toolStripStatusToolName.Text = "Инструмент";
            // 
            // toolStripStatusPlaceholder
            // 
            toolStripStatusPlaceholder.Name = "toolStripStatusPlaceholder";
            toolStripStatusPlaceholder.Size = new Size(799, 17);
            toolStripStatusPlaceholder.Spring = true;
            // 
            // toolStripStatusCoords
            // 
            toolStripStatusCoords.Name = "toolStripStatusCoords";
            toolStripStatusCoords.Size = new Size(74, 17);
            toolStripStatusCoords.Text = "координаты";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, правкаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(978, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { создатьToolStripMenuItem, открытьToolStripMenuItem, сохранитьToolStripMenuItem, сохранитьКакToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // создатьToolStripMenuItem
            // 
            создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            создатьToolStripMenuItem.Size = new Size(162, 22);
            создатьToolStripMenuItem.Text = "Создать";
            создатьToolStripMenuItem.Click += создатьToolStripMenuItem_Click;
            // 
            // открытьToolStripMenuItem
            // 
            открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            открытьToolStripMenuItem.Size = new Size(162, 22);
            открытьToolStripMenuItem.Text = "Открыть";
            открытьToolStripMenuItem.Click += открытьToolStripMenuItem_Click;
            // 
            // сохранитьToolStripMenuItem
            // 
            сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            сохранитьToolStripMenuItem.Size = new Size(162, 22);
            сохранитьToolStripMenuItem.Text = "Сохранить";
            сохранитьToolStripMenuItem.Click += сохранитьToolStripMenuItem_Click;
            // 
            // сохранитьКакToolStripMenuItem
            // 
            сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            сохранитьКакToolStripMenuItem.Size = new Size(162, 22);
            сохранитьКакToolStripMenuItem.Text = "Сохранить как...";
            сохранитьКакToolStripMenuItem.Click += сохранитьКакToolStripMenuItem_Click;
            // 
            // правкаToolStripMenuItem
            // 
            правкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { отменитьToolStripMenuItem });
            правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            правкаToolStripMenuItem.Size = new Size(59, 20);
            правкаToolStripMenuItem.Text = "Правка";
            // 
            // отменитьToolStripMenuItem
            // 
            отменитьToolStripMenuItem.Enabled = false;
            отменитьToolStripMenuItem.Name = "отменитьToolStripMenuItem";
            отменитьToolStripMenuItem.Size = new Size(128, 22);
            отменитьToolStripMenuItem.Text = "Отменить";
            отменитьToolStripMenuItem.Click += отменитьToolStripMenuItem_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(numericUpDown1);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(pictureBox2);
            panel2.Location = new Point(731, 27);
            panel2.Name = "panel2";
            panel2.Size = new Size(235, 503);
            panel2.TabIndex = 4;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(145, 398);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(49, 23);
            numericUpDown1.TabIndex = 13;
            numericUpDown1.TabStop = false;
            numericUpDown1.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 400);
            label4.Name = "label4";
            label4.Size = new Size(84, 15);
            label4.TabIndex = 12;
            label4.Text = "Размер кисти:";
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Location = new Point(145, 309);
            panel3.Name = "panel3";
            panel3.Size = new Size(75, 83);
            panel3.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 372);
            label3.Name = "label3";
            label3.Size = new Size(14, 15);
            label3.TabIndex = 7;
            label3.Text = "B";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 341);
            label2.Name = "label2";
            label2.Size = new Size(15, 15);
            label2.TabIndex = 7;
            label2.Text = "G";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 312);
            label1.Name = "label1";
            label1.Size = new Size(14, 15);
            label1.TabIndex = 6;
            label1.Text = "R";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(5, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(226, 300);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            pictureBox2.Paint += pictureBox2_Paint;
            pictureBox2.MouseDown += pictureBox2_MouseDown;
            pictureBox2.MouseUp += pictureBox2_MouseUp;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(pictureBox1);
            panel4.Location = new Point(56, 27);
            panel4.Name = "panel4";
            panel4.Size = new Size(669, 503);
            panel4.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 560);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(panel1);
            DoubleBuffered = true;
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(900, 526);
            Name = "Form1";
            Text = "GraphicEditor";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Panel panel1;
        private Button ellipse_btn;
        private Button rectangle_btn;
        private Button line_btn;
        private Button brush_btn;
        private Button hand_btn;
        private StatusStrip statusStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private ToolStripMenuItem правкаToolStripMenuItem;
        private ToolStripMenuItem отменитьToolStripMenuItem;
        private Panel panel2;
        private Label label1;
        private PictureBox pictureBox2;
        private Panel panel3;
        private Label label3;
        private Label label2;
        private Label label4;
        private NumericUpDown numericUpDown1;
        private ToolStripMenuItem создатьToolStripMenuItem;
        private Panel panel4;
        private ToolStripStatusLabel toolStripStatusToolIcon;
        private ToolStripStatusLabel toolStripStatusToolName;
        private ToolStripStatusLabel toolStripStatusPlaceholder;
        private ToolStripStatusLabel toolStripStatusCoords;
        private Button eraser_btn;
    }
}