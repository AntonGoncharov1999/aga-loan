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
    public partial class pomes : Form
    {
        public pomes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            summ.Text = "";
            srok.Text = "";
            stav.Text = "";
            txtResult.Text = "";
        }

        void parses()
        {
            var dan = new HttpRequest();
            string response = dan.Get("https://www.banki.ru/products/currency/cash/ekaterinburg/").ToString();

            HtmlAgilityPack.HtmlDocument hap = new HtmlAgilityPack.HtmlDocument();
            hap.LoadHtml(response);

            File.WriteAllText("st.html", response, Encoding.UTF8);
            string usd = hap.DocumentNode.QuerySelector("div.currency-table__large-text").InnerHtml;

            label16.Text = usd;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // od — возврат основного долга; sk — первоначальная сумма кредита; kp — количество периодов
            // np — начисленные проценты; ok — остаток кредита в данном месяце; ps — годовая процентная ставка.
            //od = b  sk = s  kp = n 
            //np = p  ok = sn  ps = pp
            //b = S / N               od = sk / kp
            //p = Sn * pp / 12        np = ok * (ps / 12)
            //Sn = S — (b * n)        ok = sk - (od * kp)

            int n =0;
            double a = 0.1 ;

            int S = Convert.ToInt32(summ.Text);
            int N = Convert.ToInt32(srok.Text);
            int pp = Convert.ToInt32(stav.Text);

             if (N <= 0)
            {
                label4.Text = "Введено некоректный период займа";
            }
            else
            {
                double b = S / N;
                double sn = S - (b * n);
                double p = sn * pp / 12;

                double[] res = new double[N];

                for (int i = 0; i < N; i++)
                {
                    res[i] = b + (S - (b * i)) * a / 12;
                    int mes = 1 + i;
                    txtResult.Text += mes + " месяц оплата = " + res[i] +"\n";

                }
            }           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(txtResult.Text, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new PointF(100, 100));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void pomes_Load(object sender, EventArgs e)
        {
            parses();
        }
    }
}
