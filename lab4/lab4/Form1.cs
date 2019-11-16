using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int k = 24, n, p, dmin = 3, x, y;
        double[,] pnk, hp;
        double[] a, b, s;

        private void button1_Click(object sender, EventArgs e)
        {
            Calc_n_p();
            Calc_pnk();
        }

        void Calc_n_p()
        {
            n = k; //мб n = k
            while (Math.Pow(2, k) > (Math.Pow(2, n) / (n + 1)))
                n++;
            p = n - k;
        }

        void Calc_pnk()
        {
            x = p;
            int pp = 0;
            y = Convert.ToInt32(Math.Pow(2, p));
            double[,] prov = new double[y, x + 1];
            for (int i = 0; i < y; i++)
            {
                pp = i;
                for (int j = x - 1; j >= 0; j--)
                {
                    prov[i, j] = pp % 2;
                    if (prov[i, j] == 1) prov[i, x]++;
                    pp /= 2;
                    
                }
            }
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x + 1; j++)
                {
                    richTextBox1.AppendText(prov[i, j].ToString());
                }
                richTextBox1.AppendText("\n");
            }
        }

    }
}
