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
        double[,] pnk, h;
        double[] a, b, word, sys_code, mis, s;
        Random rand = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            Calc_n_p();
            Calc_pnk();
            Calc_H();
            Output_pnk();
            Output_H();
            Calc_word_sys_code();
            Output_word_sys_code();
            Calc_mis();
            Output_mis();
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
        }

        void Calc_H()
        {
            h = new double[p, n];
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < k; j++)
                    h[i, j] = pnk[j, (k - 0) + i];
                h[i, k + i] = 1;
            }
        }

        void Calc_word_sys_code()
        {
            word = new double[k];
            sys_code = new double[n];
            b = new double[p];
            for (int i = 0; i < k; i++)
                word[i] = rand.Next(2);

            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < k; j++)
                    if (h[i, j] == 1) b[i] += word[j];
                if (b[i] % 2 == 0) b[i] = 0;
                else b[i] = 1;
            }
            int g = 0;
            for (int i = 0; i < n; i++)
                if (i < k) sys_code[i] = word[i];
                else
                {
                    sys_code[i] = b[g];
                    g++;
                }
        }

        void Calc_mis()
        {
            mis = new double[n];
            s = new double[p];
            mis = sys_code;
            int err = rand.Next(n);
            if (mis[err] == 1) mis[err] = 0;
            else mis[err] = 1;
     
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < n; j++)
                    if (h[i, j] == 1) s[i] += mis[j];
                if (s[i] % 2 == 0) s[i] = 0;
                else s[i] = 1;
            }
            
        }

        void Output_pnk()
        {
            richTextBox1.Clear();
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == k) richTextBox1.AppendText(" | ");
                    richTextBox1.AppendText(pnk[i, j].ToString());
                }
                richTextBox1.AppendText("\n");
            }
        }

        void Output_H()
        {
            richTextBox2.Clear();
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == k) richTextBox2.AppendText(" | ");
                    richTextBox2.AppendText(h[i, j].ToString() + " ");
                }
                richTextBox2.AppendText("\n");
            }
        }

        void Output_word_sys_code()
        {
            richTextBox3.Clear();
            for (int i = 0; i < k; i++)
                richTextBox3.AppendText(word[i].ToString());
            richTextBox3.AppendText(" - Комбинация\n");
            for (int i = 0; i < n; i++)
                richTextBox3.AppendText(sys_code[i].ToString());
            richTextBox3.AppendText(" - Сист. код\n\n");
        }
        void Output_mis()
        {
            for (int i = 0; i < n; i++)
                richTextBox3.AppendText(mis[i].ToString());
            richTextBox3.AppendText(" - код с ошибкой\n\n");
            for (int i = 0; i < p; i++)
                richTextBox3.AppendText(s[i].ToString());
            richTextBox3.AppendText(" - синдром ошибки\n\n");
        }
    }
}
