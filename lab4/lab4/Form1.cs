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

        int k = 5, n, p, dmin = 3, x, y;
        double[,] pnk, hp;
        double[] a, b, s;
        Random rand = new Random();
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
            pnk = new double[k, n];
            for (int i = 0; i < k; i++) pnk[i, i] = 1;
            double[,] prov = new double[y, x + 2];
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
            for (int i = 0; i < k; i++)
            {
                int h = rand.Next(y);
                if (prov[h, x] >= 2 && prov[h, x + 1] == 0)
                {
                    int g = 0;
                    for (int j = k; j < n; j++)
                    {
                        pnk[i, j] = prov[h, g];
                        g++;
                    }
                    prov[h, x + 1] = 1;
                }
                else i--;
            }
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < n; j++) richTextBox1.AppendText(pnk[i,j].ToString());
                richTextBox1.AppendText("\n");
            }
        }

    }
}
