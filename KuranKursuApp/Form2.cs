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
    public partial class Form2 : Form
    {
        
        OgrenciKayit kyt = new OgrenciKayit();
        HocaKayit hocakyt = new HocaKayit();
        OgrenciGuncelleme ogrguncelleme = new OgrenciGuncelleme();
        public Form2()
        {
            InitializeComponent();
        }
        bool dragging;
        Point offset;
        private void Form2_MouseDown(object sender, MouseEventArgs e)
        { dragging = true; offset = e.Location; }
        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }
        

        private void label4_Click(object sender, EventArgs e)
        {
            Form2.ActiveForm.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kyt.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        

        private void hoca_kayit_Click(object sender, EventArgs e)
        {
            hocakyt.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ogrguncelleme.Show();
        }
    }
}
