using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArcEngineProgram
{
    public partial class FormControlPoints : Form
    {
        public FormControlPoints()
        {
            InitializeComponent();
        }

        private void AddControlPoint_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalData.x = Convert.ToDouble(textBox1.Text);
            GlobalData.y = Convert.ToDouble(textBox2.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
