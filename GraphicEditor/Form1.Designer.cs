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
            ellipse_btn = new Button();
            rectangle_btn = new Button();
            line_btn = new Button();
            brush_btn = new Button();
            hand_btn = new Button();
            statusStrip1 = new StatusStrip();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Location = new Point(142, 27);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(748, 555);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(ellipse_btn);
            panel1.Controls.Add(rectangle_btn);
            panel1.Controls.Add(line_btn);
            panel1.Controls.Add(brush_btn);
            panel1.Controls.Add(hand_btn);
            panel1.Location = new Point(12, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(124, 555);
            panel1.TabIndex = 1;
            // 
            // ellipse_btn
            // 
            ellipse_btn.Location = new Point(3, 119);
            ellipse_btn.Name = "ellipse_btn";
            ellipse_btn.Size = new Size(43, 23);
            ellipse_btn.TabIndex = 4;
            ellipse_btn.Text = "button5";
            ellipse_btn.UseVisualStyleBackColor = true;
            ellipse_btn.Click += ellipse_btn_Click;
            // 
            // rectangle_btn
            // 
            rectangle_btn.Location = new Point(3, 90);
            rectangle_btn.Name = "rectangle_btn";
            rectangle_btn.Size = new Size(43, 23);
            rectangle_btn.TabIndex = 3;
            rectangle_btn.Text = "button4";
            rectangle_btn.UseVisualStyleBackColor = true;
            rectangle_btn.Click += rectangle_btn_Click;
            // 
            // line_btn
            // 
            line_btn.Location = new Point(3, 61);
            line_btn.Name = "line_btn";
            line_btn.Size = new Size(43, 23);
            line_btn.TabIndex = 2;
            line_btn.Text = "button3";
            line_btn.UseVisualStyleBackColor = true;
            line_btn.Click += line_btn_Click;
            // 
            // brush_btn
            // 
            brush_btn.Location = new Point(3, 32);
            brush_btn.Name = "brush_btn";
            brush_btn.Size = new Size(43, 23);
            brush_btn.TabIndex = 1;
            brush_btn.Text = "button2";
            brush_btn.UseVisualStyleBackColor = true;
            brush_btn.Click += brush_btn_Click;
            // 
            // hand_btn
            // 
            hand_btn.Location = new Point(3, 3);
            hand_btn.Name = "hand_btn";
            hand_btn.Size = new Size(43, 23);
            hand_btn.TabIndex = 0;
            hand_btn.Text = "button1";
            hand_btn.UseVisualStyleBackColor = true;
            hand_btn.Click += hand_btn_click;
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 598);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1143, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, правкаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1143, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { открытьToolStripMenuItem, сохранитьToolStripMenuItem, сохранитьКакToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            открытьToolStripMenuItem.Size = new Size(162, 22);
            открытьToolStripMenuItem.Text = "Открыть";
            // 
            // сохранитьToolStripMenuItem
            // 
            сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            сохранитьToolStripMenuItem.Size = new Size(162, 22);
            сохранитьToolStripMenuItem.Text = "Сохранить";
            // 
            // сохранитьКакToolStripMenuItem
            // 
            сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            сохранитьКакToolStripMenuItem.Size = new Size(162, 22);
            сохранитьКакToolStripMenuItem.Text = "Сохранить как...";
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
            отменитьToolStripMenuItem.Name = "отменитьToolStripMenuItem";
            отменитьToolStripMenuItem.Size = new Size(128, 22);
            отменитьToolStripMenuItem.Text = "Отменить";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(numericUpDown1);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(pictureBox2);
            panel2.Location = new Point(896, 27);
            panel2.Name = "panel2";
            panel2.Size = new Size(235, 555);
            panel2.TabIndex = 4;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(145, 443);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(49, 23);
            numericUpDown1.TabIndex = 13;
            numericUpDown1.TabStop = false;
            numericUpDown1.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 445);
            label4.Name = "label4";
            label4.Size = new Size(84, 15);
            label4.TabIndex = 12;
            label4.Text = "Размер кисти:";
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Location = new Point(145, 333);
            panel3.Name = "panel3";
            panel3.Size = new Size(75, 83);
            panel3.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 396);
            label3.Name = "label3";
            label3.Size = new Size(14, 15);
            label3.TabIndex = 7;
            label3.Text = "B";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 365);
            label2.Name = "label2";
            label2.Size = new Size(15, 15);
            label2.TabIndex = 7;
            label2.Text = "G";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 336);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 620);
            Controls.Add(panel2);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            DoubleBuffered = true;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "GraphicEditor";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
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
    }
}