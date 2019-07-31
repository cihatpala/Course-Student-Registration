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
    public partial class OgrenciKayit : Form
    {
        string hedef = Application.StartupPath + @"\img\"; //Fotoğrafların uzantıları
        //Veritabanından kayıt fotoğrafı için hedef+"kayit_"+tcNo ile ulaşmayı hedefliyorum...
        //Veritabanından mezun fotoğrafı için hedef+"mezun_"+tcNo ile ulaşmayı hedefliyorum... (Umarım olur.)
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=kutlukursu;Uid=root;Pwd=;sslmode=none;Charset=utf8");
        MySqlCommand cmd = new MySqlCommand();
        
        private void OgrenciKayit_Load(object sender, EventArgs e)
        {
            //Combobox Dolduran kodlar
            MySqlCommand komut = new MySqlCommand();
            komut.CommandText = "select * from hocalar";
            komut.Connection = con;
            komut.CommandType = CommandType.Text;
            MySqlDataReader dr;
            con.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                hoca_adsoyad.Items.Add(dr["ad_soyad"]);
            }
            con.Close();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            
            string ad = adi.Text, soyad = soyadi.Text, dogum_yeri = dogumyeri.Text, dogumtarihi = dogum_tarihi.Text;
            string memleket = memleketi.Text, adress = adresi.Text, baba_ad = baba_adi.Text, anne_ad = anne_adi.Text;
            string tc = tcsi.Text,
                ogrenci_iletisim = ogrenci_iletisim_no.Text,
                baba_iletisim = baba_iletisim_no.Text,
                anne_iletisim = anne_iletisim_no.Text;
            string kayit_tarih = kayit_tarihi.Text;
            string yuzune_tarih = yuzune_tarihi.Text;
            string hafizlik_tarih = hafizlik_tarihi.Text;
            string mezuniyet_tarih = mezuniyet_tarihi.Text;
            string hocasi = hoca_adsoyad.Text;
            if (dogum_tarihi.Text == "gg.aa.yyyy")
            {
                dogumtarihi = "";
            }
            string dosyaYolu_bosKayit,dosyaYolu_bosMezun, dosyaAdi_bosKayit,dosyaAdi_bosMezun;
            if (kayitfoto.Image == null) //kayıt fotoğrafı boş ise eğer
            {
                //Kayıt fotoğrafı yerine klasik öğrenci icon u konulması için gereken kodlar
                dosyaYolu_bosKayit = Application.StartupPath + @"\img\ogrenciboskayit.png";  
                kayitfoto.ImageLocation = dosyaYolu_bosKayit;
                dosyaAdi_bosKayit = Path.GetFileName(dosyaYolu_bosKayit); //Dosya adını alma
                string kaynak = dosyaYolu_bosKayit;
                string yeniad = "kayit_" + tcsi.Text + ".jpg"; //Tcsi ile isim verme
                File.Delete(hedef + yeniad);
                File.Copy(kaynak, hedef + yeniad);
            }
            if(mezunfoto.Image==null) // mezun fotoğrafı boş ise eğer
            {
                //Mezun fotoğrafı yerine klasik mezun öğrenci icon u konulması için gereken kodlar
                dosyaYolu_bosMezun = Application.StartupPath + @"\img\ogrencibosmezun.png";
                mezunfoto.ImageLocation = dosyaYolu_bosMezun;
                dosyaAdi_bosMezun = Path.GetFileName(dosyaYolu_bosMezun); //Dosya adını alma
                string kaynak = dosyaYolu_bosMezun;
                string yeniad = "mezun_" + tcsi.Text + ".jpg"; //Tcsi ile isim verme
                File.Delete(hedef + yeniad);
                File.Copy(kaynak, hedef + yeniad);
            }
            cmd.Connection = con;
            string sql = "insert into ogrenciler(ad,soyad,tc_no,dogum_yeri,memleketi,dogum_tarihi,ogrenci_iletisim,adress,baba_adi,anne_adi,baba_iletisim,anne_iletisim,kursa_kayit,yuzune_kayit,hafizlik_kayit,mezuniyet,hocasi) values('" + ad + "', '" + soyad + "', '" + tc + "','" + dogum_yeri + "', '" + memleket + "', '" + dogumtarihi + "','" + ogrenci_iletisim + "', '" + adress + "', '" + baba_ad + "','" + anne_ad + "', '" + baba_iletisim + "', '" + anne_iletisim + "','" + kayit_tarih + "', '" + yuzune_tarih + "', '" + hafizlik_tarih + "','" + mezuniyet_tarih + "', '" + hocasi + "')";
            con.Open();
            cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Kayıt Başarılı!");
            con.Close();
            
            adi.Text = soyadi.Text = tcsi.Text = dogumyeri.Text = memleketi.Text = ogrenci_iletisim_no.Text = anne_adi.Text
                = baba_adi.Text = anne_iletisim_no.Text = baba_iletisim_no.Text = adresi.Text = hoca_adsoyad.Text = "";
            kayit_tarihi.Value = yuzune_tarihi.Value = hafizlik_tarihi.Value = mezuniyet_tarihi.Value = DateTime.Today;
            dogum_tarihi.Text = "gg.aa.yyyy";
            kayitfoto.Image = null;
            mezunfoto.Image = null;
        }
        public OgrenciKayit()
        {
            InitializeComponent();
        }
        bool dragging;
        Point offset;
        private void OgrenciKayit_MouseDown(object sender, MouseEventArgs e)
        { dragging = true; offset = e.Location; }
        private void OgrenciKayit_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void OgrenciKayit_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            OgrenciKayit.ActiveForm.Hide();
            adi.Text = soyadi.Text = tcsi.Text = dogumyeri.Text = memleketi.Text = ogrenci_iletisim_no.Text = anne_adi.Text
                = baba_adi.Text = anne_iletisim_no.Text = baba_iletisim_no.Text = adresi.Text = hoca_adsoyad.Text = "";
            kayit_tarihi.Value=yuzune_tarihi.Value=hafizlik_tarihi.Value=mezuniyet_tarihi.Value = DateTime.Today;
            dogum_tarihi.Text = "gg.aa.yyyy";
            kayitfoto.Image = null;
            mezunfoto.Image = null;
        }

        private void dogum_tarihi_Click(object sender, EventArgs e)
        {
            dogum_tarihi.Text = "";
        }

        private void dogum_tarihi_TextChanged(object sender, EventArgs e)
        {

        }
        string dosyaYolu_kayit,dosyaYolu_mezun;
        string dosyaAdi_kayit,dosyaAdi_mezun;
       
        private void btn_kyt_foto_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png | Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            dosyaYolu_kayit = dosya.FileName;
            kayitfoto.ImageLocation = dosyaYolu_kayit;
        }

        private void btn_kyt_onay_Click(object sender, EventArgs e)
        {
            dosyaAdi_kayit = Path.GetFileName(dosyaYolu_kayit); //Dosya adını alma
            string kaynak = dosyaYolu_kayit;  
            string yeniad ="kayit_"+ tcsi.Text + ".jpg"; //Tcsi ile isim verme
            File.Delete(hedef + yeniad);
            File.Copy(kaynak, hedef + yeniad);
            MessageBox.Show("Kayıt fotoğrafı başarıyla eklendi.");
        }

        private void btn_mezun_foto_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png | Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            dosyaYolu_mezun = dosya.FileName;
            mezunfoto.ImageLocation = dosyaYolu_mezun;
        }

        private void btn_mezun_onay_Click(object sender, EventArgs e)
        {
            dosyaAdi_mezun = Path.GetFileName(dosyaYolu_mezun); //Dosya adını alma
            string kaynak = dosyaYolu_mezun;
            string yeniad = "mezun_" + tcsi.Text + ".jpg"; //Tcsi ile isim verme
            File.Delete(hedef + yeniad);
            File.Copy(kaynak, hedef + yeniad);
            MessageBox.Show("Mezuniyet fotoğrafı başarıyla eklendi.");
        }
    }
}
