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

namespace Odev2._4
{
    public partial class OgrenciKayitSistemi : Form
    {
         
        public OgrenciKayitSistemi()
        {
            InitializeComponent();
        }

        private void btn_ogrencikaydet_Click(object sender, EventArgs e)
        {
            string cinsiyet = rb_kadin.Checked ? "Kadın" : rb_erkek.Checked ? "Erkek" : "Cinsiyet Seçilmedi";
            string dogumTarihi = mc_dogumtarihi.SelectionStart.ToShortDateString();
            string kayitdurumu = rb_aktif.Checked ? "Aktif" : rb_pasif.Checked ? "Pasif" : "Kayıt durumu seçilmedi";

            string ogrencibilgileri = tb_isim.Text + "\n" +
                          tb_soyisim.Text + "\n" +
                          mtb_kimlik.Text + "\n" +
                          cinsiyet + "\n" +
                          cb_dogumyeri.Text + "\n" +
                          dogumTarihi + "\n" +
                          mtb_telefonno.Text + "\n" +
                          tb_email.Text + "\n" +
                          tb_adres.Text + "\n" +
                          tb_veliadi.Text + "\n" +
                          tb_velisoyadi.Text + "\n" +
                          mtb_velitelefon.Text + "\n" +
                          tb_velimeslek.Text + "\n" +
                          tb_veliadres.Text + "\n" +
                          cb_ogrenimdurumu.Text + "\n" +
                          tb_oncekiokul.Text + "\n" +
                          dtp_kayittarihi.Text + "\n" +
                          cb_ogrencino.Text + "\n" +
                          kayitdurumu + "\n" +
                          cb_kangrubu.Text + "\n" +
                          tb_alerjiler.Text + "\n" +
                          tb_hastalik.Text + "\n";



            string path = "/Öğrenci Kayıtları";

            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                di.Create();
            }
            string dosyadi = $"{tb_isim.Text}_{tb_soyisim.Text}_{mtb_kimlik.Text}.txt";
            string dosyayolu = path + "\\" + dosyadi;


            File.WriteAllText(dosyayolu, ogrencibilgileri);
            MessageBox.Show("Öğrenci Kaydedildi", "Kayıt", MessageBoxButtons.OK);


        }

        private void btn_iptal_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Kayıt işlemine devam etmek istiyor musun?", "İptal", MessageBoxButtons.YesNo);

            if (sonuc == DialogResult.No)
            {
                tb_isim.Text = "";
                tb_soyisim.Text = "";
                mtb_kimlik.Text = "";
                rb_kadin.Checked = false;
                rb_erkek.Checked = false;
                cb_dogumyeri.SelectedIndex = -1;
                mc_dogumtarihi.SetDate(DateTime.Now);
                mtb_telefonno.Text = "";
                tb_email.Text = "";
                tb_adres.Text = "";
                tb_veliadi.Text = "";
                tb_velisoyadi.Text = "";
                mtb_velitelefon.Text = "";
                tb_velimeslek.Text = "";
                tb_veliadres.Text = "";
                cb_ogrenimdurumu.SelectedIndex = -1;
                tb_oncekiokul.Text = "";
                dtp_kayittarihi.Value = DateTime.Now;
                cb_ogrencino.Text = "";
                rb_aktif.Checked = false;
                rb_pasif.Checked = false;
                cb_kangrubu.SelectedIndex = -1;
                tb_alerjiler.Text = "";
                tb_hastalik.Text = "";
            }
        }

        private void btn_bilgileriyazdir_Click(object sender, EventArgs e)
        {
            if (lb_ogrenciler.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir öğrenci seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedOgrenci = lb_ogrenciler.SelectedItem.ToString();
            string ogrenciadi = selectedOgrenci.Split('.')[1].Trim();

            OgrenciBilgileriGetir(ogrenciadi);
        }

        private void btn_cikisyap_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Çıkış yapmak istediğine emin misin?", "Dikkat", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (sonuc == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Çıkış İşlemi iptal edildi", "Bilgi");
            }
        }

        private void btn_ogrencibilgi_Click(object sender, EventArgs e)
        {
            string path = "/Öğrenci Kayıtları";

            lb_ogrenciler.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] dosyalar = di.GetFiles();

            int index = 1;
            foreach (FileInfo dosya in dosyalar)
            {
                string dosyaadi = Path.GetFileNameWithoutExtension(dosya.Name);
                lb_ogrenciler.Items.Add($"{index}. {dosyaadi}");
                index++;
            }
        }

        private void lb_ogrenciler_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOgrenciAdi;
            if (lb_ogrenciler.SelectedItem != null)
            {
                string selectedogrenci = lb_ogrenciler.SelectedItem.ToString();
                selectedOgrenciAdi = selectedogrenci.Split('.')[1].Trim();

            }
        }
        private void OgrenciBilgileriGetir(string ogrenciadi)
        {
            string path = "/Öğrenci Kayıtları";
            string filePath = Path.Combine(path, ogrenciadi + ".txt");

            if (File.Exists(filePath))
            {

                string ogrenciInfo = File.ReadAllText(filePath);
                string[] bilgiler = ogrenciInfo.Split(new[] { '\n' });

                lbl_ogrencibilgileri.Text = $"İsim: {bilgiler[0].Trim()}\n" +
                                            $"Soyisim: {bilgiler[1].Trim()}\n" +
                                            $"Kimlik Numarası: {bilgiler[2].Trim()}\n" +
                                            $"Cinsiyet: {bilgiler[3].Trim()}\n" +
                                            $"Doğum Yeri: {bilgiler[4].Trim()}\n" +
                                            $"Doğum Tarihi: {bilgiler[5].Trim()}\n" +
                                            $"Telefon No: {bilgiler[6].Trim()}\n" +
                                            $"E-mail: {bilgiler[7].Trim()}\n" +
                                            $"Adres: {bilgiler[8].Trim()}\n" +
                                            $"Veli Adı: {bilgiler[9].Trim()}\n" +
                                            $"Veli Soyadı: {bilgiler[10].Trim()}\n" +
                                            $"Veli Telefon: {bilgiler[11].Trim()}\n" +
                                            $"Veli Meslek: {bilgiler[12].Trim()}\n" +
                                            $"Veli Adres: {bilgiler[13].Trim()}\n" +
                                            $"Öğrenim Durumu: {bilgiler[14].Trim()}\n" +
                                            $"Önceki Okul: {bilgiler[15].Trim()}\n" +
                                            $"Kayıt Tarihi: {bilgiler[16].Trim()}\n" +
                                            $"Öğrenci No: {bilgiler[17].Trim()}\n" +
                                            $"Kayıt Durumu: {bilgiler[18].Trim()}\n" +
                                            $"Kan Grubu: {bilgiler[19].Trim()}\n" +
                                            $"Alerjiler: {bilgiler[20].Trim()}\n" +
                                            $"Hastalık: {bilgiler[21].Trim()}";

            }
            else
            {
                MessageBox.Show("Seçilen öğrenci bilgileri bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OgrenciKayitSistemi_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Çıkış Yapmak istediğine emin misin?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sonuc == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
