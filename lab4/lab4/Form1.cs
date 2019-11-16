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
        int k = 5, n, p, x, y, kol, kp;
        int[]  mis_ind;
        double[,] pnk, h;
        double[,] b, word, sys_code, mis, s;

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            kp = Convert.ToInt32(numericUpDown3.Value) - 1;
            Output_word_sys_code(kp);
            Output_H(kp);
            Output_mis(kp);
            
        }

        Random rand = new Random();

        private void button1_Click(object sender, EventArgs e)
        {
            k = Convert.ToInt32(numericUpDown1.Value);
            kol = Convert.ToInt32(numericUpDown2.Value);
            numericUpDown3.Maximum = numericUpDown2.Value;
            kp = 0;
            Calc_n_p();
            Calc_pnk();
            Calc_H();
            Output_pnk();
            Calc_word_sys_code();
           
            Calc_mis();
             Output_word_sys_code(kp);
            Output_mis(kp);
            Output_H(kp);
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
            word = new double[kol, k];
            sys_code = new double[kol, n];
            b = new double[kol, p];
            for (int kpp = 0; kpp < kol; kpp++)
            {
               
                for (int i = 0; i < k; i++)
                    word[kpp, i] = rand.Next(2);

                for (int i = 0; i < p; i++)
                {
                    for (int j = 0; j < k; j++)
                        if (h[i, j] == 1) b[kpp, i] += word[kpp, j];
                    if (b[kpp, i] % 2 == 0) b[kpp, i] = 0;
                    else b[kpp, i] = 1;
                }
                int g = 0;
                for (int i = 0; i < n; i++)
                    if (i < k) sys_code[kpp, i] = word[kpp, i];
                    else
                    {
                        sys_code[kpp, i] = b[kpp, g];
                        g++;
                    }
            }
        }

        void Calc_mis()
        {
            mis = new double[kol, n];
            s = new double[kol, p];
            mis_ind = new int[kol];
            for (int i = 0; i < kol; i++)
                for (int j = 0; j < n; j++)
                    mis[i, j] = sys_code[i, j];
            for (int kpp = 0; kpp < kol; kpp++)
            {
                int err = rand.Next(n);
                if (mis[kpp, err] == 1) mis[kpp, err] = 0;
                else mis[kpp, err] = 1;

                for (int i = 0; i < p; i++)
                {
                    for (int j = 0; j < n; j++)
                        if (h[i, j] == 1) s[kpp, i] += mis[kpp, j];
                    if (s[kpp, i] % 2 == 0) s[kpp, i] = 0;
                    else s[kpp, i] = 1;
                }
                for (int j = 0; j < n; j++)
                {
                    int v = 0;
                    for (int i = 0; i < p; i++) if (h[i, j] == s[kpp, i]) v++;
                    if (v == p) mis_ind[kpp] = j;
                }
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

        void Output_H(int kpp)
        {
            richTextBox2.Clear();
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == k) richTextBox2.AppendText(" | ");
                    richTextBox2.AppendText(h[i, j].ToString() + " ");
                    richTextBox2.Select(richTextBox2.TextLength - 2, 1);
                    richTextBox2.SelectionColor = Color.Black;
                    if (j == mis_ind[kpp]) richTextBox2.SelectionColor = Color.Red;
                }
                richTextBox2.AppendText("\n");
            }
        }

        void Output_word_sys_code(int kpp)
        {
            richTextBox3.Clear();
            for (int i = 0; i < k; i++)
                richTextBox3.AppendText(word[kpp, i].ToString());
            richTextBox3.AppendText(" - комбинация\n");
            for (int i = 0; i < n; i++)
                richTextBox3.AppendText(sys_code[kpp, i].ToString());
            richTextBox3.AppendText(" - сист. код\n");
        }
        void Output_mis(int kpp)
        {
            for (int i = 0; i < n; i++)
                richTextBox3.AppendText(mis[kpp, i].ToString());
            richTextBox3.AppendText(" - код с ошибкой\n");
            for (int i = 0; i < n; i++)
            {
                richTextBox3.AppendText(mis[kpp, i].ToString());
                richTextBox3.Select(richTextBox3.TextLength - 1, 1);
                richTextBox3.SelectionColor = Color.Black;
                if (i == mis_ind[kpp]) richTextBox3.SelectionColor = Color.Red;
            }
            richTextBox3.AppendText(" - ошибка\n\n");
            richTextBox3.Select(richTextBox3.TextLength - 11, 11);
            richTextBox3.SelectionColor = Color.Black;
            
             for (int i = 0; i < p; i++)
                richTextBox3.AppendText(s[kpp, i].ToString());
            richTextBox3.AppendText(" - синдром ошибки\n\n");
        }
    }
}
