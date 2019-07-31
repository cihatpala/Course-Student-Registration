using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.OleDb;

namespace KuranKursuApp
{
    public partial class OgrenciGuncelleme : Form
    {
        string hedef = Application.StartupPath + @"\img\";
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=kutlukursu;Uid=root;Pwd=;sslmode=none;Charset=utf8");
        MySqlCommand cmd = new MySqlCommand();
        DataTable dt = new DataTable();
        private void kayit_sil_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            string silme = "delete from ogrenciler where ogrenciler.tc_no=('"+tcsi.Text+"')";
            cmd = new MySqlCommand(silme,con);
            con.Open();
            con.Close();
            MessageBox.Show(tcsi.Text+" Tc Numaralı Öğrenci Başarıyla Silindi.");
        }
        private void kayit_guncelle_Click(object sender, EventArgs e)
        {
            int secilialan = dataGridView1.SelectedCells[0].RowIndex;
            
            string tc = dataGridView1.Rows[secilialan].Cells[2].Value.ToString();
            con.Open();
            
            string guncelle = "update ogrenciler set ad='" + adi.Text + "',adress='" + adresi.Text + "',soyad='" + soyadi.Text + "',tc_no='" + tcsi.Text + "',dogum_yeri='" + dogumyeri.Text + "',memleketi='" + memleketi.Text + "',dogum_tarihi='" + dogum_tarihi.Text + "',ogrenci_iletisim='" + ogrenci_iletisim_no.Text + "',baba_adi='" + baba_adi.Text + "',anne_adi='" + anne_adi.Text + "',baba_iletisim='" + baba_iletisim_no.Text + "',anne_iletisim='" + anne_iletisim_no.Text + "',kursa_kayit='" + kayit_tarihi.Text + "',yuzune_kayit='" + yuzune_tarihi.Text + "',hafizlik_kayit='" + hafizlik_tarihi.Text + "',mezuniyet='" + mezuniyet_tarihi.Text + "',hocasi='" + hoca_adsoyad.Text + "'where tc_no ='" + tc + "'";
            cmd = new MySqlCommand(guncelle, con);
            
            cmd.ExecuteNonQuery();
            verilerigoster("select * from ogrenciler");
            con.Close();
            dataGridView1.Rows[secilialan].Selected = true;
            textBox1.Text = "";
            textBox2.Text = "";
           
        }


        private void OgrenciGuncelleme_Load(object sender, EventArgs e)
        {
            
            dataGridView1.AllowUserToAddRows = false;
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

            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from ogrenciler",con);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
        }
       
        MySqlDataAdapter adapter;
        public void verilerigoster(string deger)
        {
            adapter = new MySqlDataAdapter(deger,con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilialan = dataGridView1.SelectedCells[0].RowIndex;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;


            string ad = dataGridView1.Rows[secilialan].Cells[0].Value.ToString();
            string soyad = dataGridView1.Rows[secilialan].Cells[1].Value.ToString();
            string tc = dataGridView1.Rows[secilialan].Cells[2].Value.ToString();
            string dogumYer = dataGridView1.Rows[secilialan].Cells[3].Value.ToString();
            string memleket = dataGridView1.Rows[secilialan].Cells[4].Value.ToString();
            string dogumtarih = dataGridView1.Rows[secilialan].Cells[5].Value.ToString();
            string ogrenciIletisim = dataGridView1.Rows[secilialan].Cells[6].Value.ToString();
            string adres = dataGridView1.Rows[secilialan].Cells[7].Value.ToString();
            string baba_ad = dataGridView1.Rows[secilialan].Cells[8].Value.ToString();
            string anne_ad = dataGridView1.Rows[secilialan].Cells[9].Value.ToString();
            string babaIletisim = dataGridView1.Rows[secilialan].Cells[10].Value.ToString();
            string anneIletisim = dataGridView1.Rows[secilialan].Cells[11].Value.ToString();
            string kursKayit = dataGridView1.Rows[secilialan].Cells[12].Value.ToString();
            string yuzuneKayit= dataGridView1.Rows[secilialan].Cells[13].Value.ToString();
            string hafizlikKayit = dataGridView1.Rows[secilialan].Cells[14].Value.ToString();
            string mezunTarih = dataGridView1.Rows[secilialan].Cells[15].Value.ToString();
            string hocasi = dataGridView1.Rows[secilialan].Cells[16].Value.ToString();

            adi.Text = ad; soyadi.Text = soyad; tcsi.Text = tc; dogumyeri.Text = dogumYer; memleketi.Text = memleket;dogum_tarihi.Text = dogumtarih;
            ogrenci_iletisim_no.Text = ogrenciIletisim;adresi.Text = adres; baba_adi.Text = baba_ad;anne_adi.Text = anne_ad;
            baba_iletisim_no.Text = babaIletisim;anne_iletisim_no.Text = anneIletisim; kayit_tarihi.Text = kursKayit;
            yuzune_tarihi.Text = yuzuneKayit; hafizlik_tarihi.Text = hafizlikKayit;
            mezuniyet_tarihi.Text = mezunTarih; hoca_adsoyad.Text = hocasi;
            //kayitfoto.Image = Image.FromFile(Application.StartupPath + @"\img\" + "kayit_" + tcsi.Text + ".jpg");
            




        }
        public OgrenciGuncelleme()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OgrenciKayit.ActiveForm.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        
        string dosyaYolu_kayit, dosyaYolu_mezun;
        string dosyaAdi_kayit, dosyaAdi_mezun;
       public OpenFileDialog dosya;
        private void btn_kyt_foto_Click(object sender, EventArgs e)
        {
            dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png | Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            dosyaYolu_kayit = dosya.FileName;
            kayitfoto.ImageLocation = dosyaYolu_kayit;               
        }
       
        private void btn_kyt_onay_Click(object sender, EventArgs e)
        {
           
            string yeniad = "kayit_" + tcsi.Text + ".jpg";
            //    File.Open("C:\\Users\\pala\\Desktop\\KuranKursuApp\\KuranKursuApp\\KuranKursuApp\\bin\\Debug\\img\\kayit_11111111111.jpg", FileMode.Open);
             
            // dosyaAdi_kayit = Path.GetFileName(dosyaYolu_kayit);
            
            string kaynak = dosyaYolu_kayit;
            //File.Delete(hedef + yeniad);
            File.Delete(hedef+yeniad);        
            File.Copy(kaynak, hedef + yeniad);
            MessageBox.Show("Kayıt fotoğrafı başarıyla güncellendi.");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            con.Open();
            MySqlCommand komut = new MySqlCommand("Select * from ogrenciler where tc_no like '%"+textBox1.Text+"%'",con);
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int harfsayisi = textBox2.Text.Length;
            con.Open();
            MySqlCommand komut = new MySqlCommand("Select * from ogrenciler where ad like '%" + textBox2.Text + "%'", con);
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            
            con.Close();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
           
                    int secilialan = dataGridView1.SelectedCells[0].RowIndex+1;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.MultiSelect = false;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.AllowUserToDeleteRows = false;
                    dataGridView1.ReadOnly = true;
                    

                    if (secilialan == dataGridView1.RowCount)
                    {
                            secilialan--;      
                    }
                    
                    string ad = dataGridView1.Rows[secilialan].Cells[0].Value.ToString();
                    string soyad = dataGridView1.Rows[secilialan].Cells[1].Value.ToString();
                    string tc = dataGridView1.Rows[secilialan].Cells[2].Value.ToString();
                    string dogumYer = dataGridView1.Rows[secilialan].Cells[3].Value.ToString();
                    string memleket = dataGridView1.Rows[secilialan].Cells[4].Value.ToString();
                    string dogumtarih = dataGridView1.Rows[secilialan].Cells[5].Value.ToString();
                    string ogrenciIletisim = dataGridView1.Rows[secilialan].Cells[6].Value.ToString();
                    string adres = dataGridView1.Rows[secilialan].Cells[7].Value.ToString();
                    string baba_ad = dataGridView1.Rows[secilialan].Cells[8].Value.ToString();
                    string anne_ad = dataGridView1.Rows[secilialan].Cells[9].Value.ToString();
                    string babaIletisim = dataGridView1.Rows[secilialan].Cells[10].Value.ToString();
                    string anneIletisim = dataGridView1.Rows[secilialan].Cells[11].Value.ToString();
                    string kursKayit = dataGridView1.Rows[secilialan].Cells[12].Value.ToString();
                    string yuzuneKayit = dataGridView1.Rows[secilialan].Cells[13].Value.ToString();
                    string hafizlikKayit = dataGridView1.Rows[secilialan].Cells[14].Value.ToString();
                    string mezunTarih = dataGridView1.Rows[secilialan].Cells[15].Value.ToString();
                    string hocasi = dataGridView1.Rows[secilialan].Cells[16].Value.ToString();

                    adi.Text = ad; soyadi.Text = soyad; tcsi.Text = tc; dogumyeri.Text = dogumYer; memleketi.Text = memleket; dogum_tarihi.Text = dogumtarih;
                    ogrenci_iletisim_no.Text = ogrenciIletisim; adresi.Text = adres; baba_adi.Text = baba_ad; anne_adi.Text = anne_ad;
                    baba_iletisim_no.Text = babaIletisim; anne_iletisim_no.Text = anneIletisim; kayit_tarihi.Text = kursKayit;
                    yuzune_tarihi.Text = yuzuneKayit; hafizlik_tarihi.Text = hafizlikKayit;
                    mezuniyet_tarihi.Text = mezunTarih; hoca_adsoyad.Text = hocasi;
            //kayitfoto.Image = Image.FromFile(Application.StartupPath + @"\img\" + "kayit_" + tcsi.Text + ".jpg");
            

            //Burda elde edilen a değeri ilgili formun textboxına aktarılır.

        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            int secilialan = dataGridView1.SelectedCells[0].RowIndex;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;

            if (secilialan == dataGridView1.RowCount)
            {
                secilialan++;
            }

            string ad = dataGridView1.Rows[secilialan].Cells[0].Value.ToString();
            string soyad = dataGridView1.Rows[secilialan].Cells[1].Value.ToString();
            string tc = dataGridView1.Rows[secilialan].Cells[2].Value.ToString();
            string dogumYer = dataGridView1.Rows[secilialan].Cells[3].Value.ToString();
            string memleket = dataGridView1.Rows[secilialan].Cells[4].Value.ToString();
            string dogumtarih = dataGridView1.Rows[secilialan].Cells[5].Value.ToString();
            string ogrenciIletisim = dataGridView1.Rows[secilialan].Cells[6].Value.ToString();
            string adres = dataGridView1.Rows[secilialan].Cells[7].Value.ToString();
            string baba_ad = dataGridView1.Rows[secilialan].Cells[8].Value.ToString();
            string anne_ad = dataGridView1.Rows[secilialan].Cells[9].Value.ToString();
            string babaIletisim = dataGridView1.Rows[secilialan].Cells[10].Value.ToString();
            string anneIletisim = dataGridView1.Rows[secilialan].Cells[11].Value.ToString();
            string kursKayit = dataGridView1.Rows[secilialan].Cells[12].Value.ToString();
            string yuzuneKayit = dataGridView1.Rows[secilialan].Cells[13].Value.ToString();
            string hafizlikKayit = dataGridView1.Rows[secilialan].Cells[14].Value.ToString();
            string mezunTarih = dataGridView1.Rows[secilialan].Cells[15].Value.ToString();
            string hocasi = dataGridView1.Rows[secilialan].Cells[16].Value.ToString();

            adi.Text = ad; soyadi.Text = soyad; tcsi.Text = tc; dogumyeri.Text = dogumYer; memleketi.Text = memleket; dogum_tarihi.Text = dogumtarih;
            ogrenci_iletisim_no.Text = ogrenciIletisim; adresi.Text = adres; baba_adi.Text = baba_ad; anne_adi.Text = anne_ad;
            baba_iletisim_no.Text = babaIletisim; anne_iletisim_no.Text = anneIletisim; kayit_tarihi.Text = kursKayit;
            yuzune_tarihi.Text = yuzuneKayit; hafizlik_tarihi.Text = hafizlikKayit;
            mezuniyet_tarihi.Text = mezunTarih; hoca_adsoyad.Text = hocasi;
            //kayitfoto.Image = Image.FromFile(Application.StartupPath + @"\img\" + "kayit_" + tcsi.Text + ".jpg");
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
    class MyClass : IDisposable
    {
        public void Dispose()
        {
            OgrenciGuncelleme.ActiveForm.Dispose();
            //Dispose kodlarımız
            GC.SuppressFinalize(this);
        }
    }
}
