using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace KuranKursuApp
{
    public partial class HocaKayit : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=kutlukursu;Uid=root;Pwd=;sslmode=none;Charset=utf8");
        MySqlCommand cmd = new MySqlCommand();


        private void button3_Click(object sender, EventArgs e)
        {
            string ad_soyad = adi_soyadi.Text, kayit_tarih = kayit_tarihi.Text, dogum_tarih = dogum_tarihi.Text;
            string tc = tcsi.Text, iletisim_no = iletisim_nosu.Text, memleket = memleketi.Text, brans = bransi.Text;
            string adres = adresi.Text;
            if (dogum_tarihi.Text == "gg.aa.yyyy")
            {
                dogum_tarih = "";
            }

            cmd.Connection = con;
            string sql = "insert into hocalar(ad_soyad,tc_no,iletisim_no,memleket,dogum_tarih,baslama_tarih,brans_alan,adres) values('" + ad_soyad + "', '" + tc + "', '" + iletisim_no + "','" + memleket + "', '" + dogum_tarih + "', '" + kayit_tarih + "','" + brans+ "','"+adres+ "')";
            con.Open();
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Kayıt Başarılı!");
            con.Close();
            adi_soyadi.Text = kayit_tarihi.Text = dogum_tarihi.Text = tcsi.Text = iletisim_nosu.Text = memleketi.Text = bransi.Text = adresi.Text = "";
            dogum_tarihi.Text = "gg.aa.yyyy";

        }
        public HocaKayit()
        {
            InitializeComponent();
        }
        bool dragging;
        Point offset;
        private void HocaKayit_MouseDown(object sender, MouseEventArgs e)
        { dragging = true; offset = e.Location; }
        private void HocaKayit_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void HocaKayit_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            } 
        }
        private void HocaKayit_Load(object sender, EventArgs e)
        {

        }

        private void lbleksi_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblkapat_Click(object sender, EventArgs e)
        {
            HocaKayit.ActiveForm.Hide();
            adi_soyadi.Text = tcsi.Text = iletisim_nosu.Text = memleketi.Text = kayit_tarihi.Text = adresi.Text = bransi.Text = "";
            dogum_tarihi.Text = "gg.aa.yyyy";
        }

        private void dogum_tarihi_Click(object sender, EventArgs e)
        {
            dogum_tarihi.Text = "";
        }
        string dosyaYolu_hoca;
        string dosyaAdi_hoca;
        string hedef = Application.StartupPath + @"\img\";
        private void btn_kyt_foto_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png | Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            dosyaYolu_hoca = dosya.FileName;
            kayitfoto.ImageLocation = dosyaYolu_hoca;
        }

        private void btn_kyt_onay_Click(object sender, EventArgs e)
        {
            dosyaAdi_hoca = Path.GetFileName(dosyaYolu_hoca); //Dosya adını alma
            string kaynak = dosyaYolu_hoca;
            string yeniad = "hoca_" + tcsi.Text + ".jpg"; //Tcsi ile isim verme
            File.Delete(hedef + yeniad);
            File.Copy(kaynak, hedef + yeniad);
            MessageBox.Show("Hoca fotoğrafı başarıyla kaydedildi.");
        }
    }
}
