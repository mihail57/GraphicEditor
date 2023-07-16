using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicEditor
{
    public partial class FileCreateForm : Form
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public FileCreateForm()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            Width = Convert.ToInt32(numericUpDown1.Value);
            Height = Convert.ToInt32(numericUpDown2.Value);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
