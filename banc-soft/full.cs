using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace banc_soft
{
    public partial class full : Form
    {
        public full()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // Mпс = Гпс / 100 / 12
            // Мп = СЗ * ( Мпс / ( 1- ( 1 + Мпс )^-СК ) )
            // Гпс = годовая процентная ставка
            // Мп – месячный платеж по займу; 
            // Сз – общее количество средств, взятых взаймы;
            // Мпс – размер месячной процентной ставки;
            // Ск – срок займа (количество месяцев) когда будут начисляться проценты по нему. 
            // переплата определяется умножением полученой суммы на количество периодов и отнять сумму займа

            double Mps, Mp, Pp ;

            //месячный платеж по займу

            double Gps = Convert.ToInt32(stavv.Text);
            double Sz = Convert.ToInt32(zaim.Text);
            double Sk = Convert.ToInt32(priod.Text);

            Mps = Gps / 100 / 12;
            Mp = Sz * ( Mps / ( 1 - (Math.Pow( 1 + Mps , -Sk ) ) ) ) ;

            //переплата 

            Pp = (Mp * Sk) -  Sz;

            
            txtValue.Text = "\b " + " Оплата в месяц = " + (Convert.ToString(Mp)) + "   " + "\n" + "\b" + "Переплата составляет = " + (Convert.ToString(Pp));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stavv.Text = "";
            zaim.Text = "";
            priod.Text = "";
            txtValue.Text = "";
        }

        void parses()
        {
            var dan = new HttpRequest();
            string response = dan.Get("https://www.banki.ru/products/currency/cash/ekaterinburg/").ToString();

            HtmlAgilityPack.HtmlDocument hap = new HtmlAgilityPack.HtmlDocument();
            hap.LoadHtml(response);

            File.WriteAllText("st.html", response, Encoding.UTF8);
            string usd = hap.DocumentNode.QuerySelector("div.currency-table__large-text").InnerHtml;

            label4.Text = usd;
        }

        private void full_Load(object sender, EventArgs e)
        {
            parses();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(txtValue.Text, new Font("Times New Roman",14, FontStyle.Bold), Brushes.Black, new PointF(100, 100));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
