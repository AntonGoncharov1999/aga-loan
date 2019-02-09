using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace banc_soft
{
    public partial class index : Form
    {
        public index()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pomes frm = new pomes();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            full frm = new full();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void index_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            o_programere frm = new o_programere();
            frm.Show();
        }
    }
}
