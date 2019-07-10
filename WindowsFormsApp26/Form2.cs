using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp26
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            pictureBox2.Image = Image.FromFile(Environment.CurrentDirectory + "/Images/back.gif");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void carbonFiberControlButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void carbonFiberTheme1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
