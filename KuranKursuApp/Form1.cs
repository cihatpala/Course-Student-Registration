using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KuranKursuApp
{
    public partial class Form1 : Form
    {
        Form2 frm = new Form2();
        OgrenciKayit ogr = new OgrenciKayit();
        OgrenciGuncelleme guncelleme = new OgrenciGuncelleme();
        public Form1()
        {
            InitializeComponent();
        }
        bool dragging;
        Point offset;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        { dragging = true; offset = e.Location; }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }
        string id = "aşıkkutlu";
        string sifre = "kutlu1901";
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textbox1.Text==id || textbox2.Text==sifre)
            {
                frm.Show();
                this.Hide();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ogr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            guncelleme.Show();
            this.Hide();
        }
    }
}
