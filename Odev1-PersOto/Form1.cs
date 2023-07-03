using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Odev1_PersOto.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Odev1_PersOto
{
    public partial class Form1 : Form
    {
        PersonelOtomContext db;
        public Form1()
        {
            InitializeComponent();
            db = new PersonelOtomContext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textID.Enabled = false;

            cmbUnvan.DataSource = Enum.GetValues(typeof(Unvan)).Cast<Unvan>().Select(x => new { Value = x, DisplayName = GetEnumDisplayName(x) }).ToList();
            cmbUnvan.DisplayMember = "DisplayName";
            cmbUnvan.ValueMember = "Value";
            cmbUnvan.SelectedIndex = -1;

            var personeller = db.PersonelBilgis.ToList();
            foreach (var personel in personeller)
            {
                ListViewItem lvi = ListviewDoldur(personel);
                lstPersoneller.Items.Add(lvi);
            }
        }

        private string GetEnumDisplayName(Unvan value)
        {
            var fieldinfo = value.GetType().GetField(value.ToString());

            var attribute = fieldinfo.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().FirstOrDefault();

            return attribute?.Name ?? value.ToString();
        }
        
        private void btnEkle_Click(object sender, EventArgs e)
        {
            PersonelBilgi personel = new PersonelBilgi();
            personel = PersonelDoldur(personel);

            ListViewItem lvi = new ListViewItem();
            lvi = ListviewDoldur(personel);

            lstPersoneller.Items.Add(lvi);
            string tcKn = txtTCKimlikNumarasi.Text.Trim();
            string ad = txtAd.Text.Trim();
            string soyad = txtSoyad.Text.Trim();
            DateTime dogumTarihi = dtDogumTarihi.Value;
            string telNo = txtTelefonNo.Text.Trim();
            string email = txtEmail.Text.Trim();
            string adres = txtAdres.Text.Trim();
            DateTime iseGirisTarih = dtIseGirisTarihi.Value;
            string unvan = cmbUnvan.Text;
            Image resim = pbResim.Image;
            try
            {
                if (!string.IsNullOrWhiteSpace(tcKn) && !string.IsNullOrWhiteSpace(ad) && !string.IsNullOrWhiteSpace(soyad))
                {
                    PersonelBilgi personel1 = new PersonelBilgi()
                    {
                        Adi = ad,
                        Soyadi = soyad,
                        Tckn = tcKn,
                        DogumTarihi = dogumTarihi,
                        Email = email,
                        TelefonNumarasi = telNo,
                        Adres = adres,
                        IseGirisTarihi = iseGirisTarih,
                        Unvan = unvan
                    };
                    db.PersonelBilgis.Add(personel1);
                    db.SaveChanges();
                }
                else
                    MessageBox.Show("Ad,Soyad ve TC Kimlik Numarasý boþ geçilemez");
            }
            catch (Exception)
            {

                MessageBox.Show("Kayýt mevcut olduðu için kayýt baþarýsýz!");
            }
            Temizle(this.Controls);
            Temizle(groupBox1.Controls);
        }

        private ListViewItem ListviewDoldur(PersonelBilgi personel)
        {
            ListViewItem lvi = new ListViewItem(personel.Tckn);
            //lvi.Text=p.TCKN;
            lvi.SubItems.Add(personel.Adi);
            lvi.SubItems.Add(personel.Soyadi);
            lvi.SubItems.Add(personel.DogumTarihi.ToString());
            lvi.SubItems.Add(personel.Email);
            lvi.SubItems.Add(personel.IseGirisTarihi.ToString());
            lvi.SubItems.Add((personel.Unvan).ToString());
            lvi.SubItems.Add(personel.TelefonNumarasi);
            lvi.SubItems.Add(personel.Adres);
            lvi.Tag = personel;
            return lvi;
        }

        private PersonelBilgi PersonelDoldur(PersonelBilgi personel)
        {
            personel.Adi = txtAd.Text.Trim();
            personel.Soyadi = txtSoyad.Text.Trim();
            personel.Tckn = txtTCKimlikNumarasi.Text.Trim();
            personel.DogumTarihi = dtDogumTarihi.Value;

            personel.Unvan = cmbUnvan.Text.ToString();
            //personel.Unvan = cmbUnvan.Text == "" ? Unvan.Belirtilmedi : (Unvan)cmbUnvan.SelectedValue;
            personel.IseGirisTarihi = dtIseGirisTarihi.Value;
            personel.Email = txtEmail.Text.Trim();
            personel.Adres = txtAdres.Text.Trim();
            personel.TelefonNumarasi = txtTelefonNo.Text.Trim();

            //if (pbResim.Tag != null)
            //{
            //    p.PersonelResmi = Guid.NewGuid() + pbResim.Tag.ToString();
            //    //Application.StartupPath veya Emvoirment.CurrentDirectory ile bin/debug a gidebiliriz.kayýt yapar.
            //    pbResim.Image.Save(Application.StartupPath + "/Images/" + p.PersonelResmi);

            //}

            return personel;
        }

        public void Temizle(Control.ControlCollection koleksiyon)
        {
            foreach (Control item in koleksiyon)
            {
                if (item is TextBox)
                    ((TextBox)item).Clear();
                else if (item is ComboBox)
                    ((ComboBox)item).SelectedIndex = -1;
                else if (item is DateTimePicker)
                    ((DateTimePicker)(item)).Value = DateTime.Now;
                else if (item is PictureBox)
                    ((PictureBox)item).Image = null;
                else if (item is GroupBox)
                    Temizle(((GroupBox)item).Controls);

            }
        }

        PersonelBilgi bilgiGuncelle;
        int secilenIndex;
        private void lstPersoneller_DoubleClick(object sender, EventArgs e)
        {
            secilenIndex = lstPersoneller.SelectedItems[0].Index;
            bilgiGuncelle = (PersonelBilgi)lstPersoneller.SelectedItems[0].Tag;
            int id = bilgiGuncelle.PersonelId;
            string idi = Convert.ToString(id);
            textID.Text = idi;
            txtAd.Text = bilgiGuncelle.Adi;
            txtSoyad.Text = bilgiGuncelle.Soyadi;
            txtEmail.Text = bilgiGuncelle.Email;
            txtAdres.Text = bilgiGuncelle.Adres;
            txtTCKimlikNumarasi.Text = bilgiGuncelle.Tckn;
            txtTelefonNo.Text = bilgiGuncelle.TelefonNumarasi;
            dtDogumTarihi.Value = bilgiGuncelle.DogumTarihi.Value;
            dtIseGirisTarihi.Value = bilgiGuncelle.IseGirisTarihi.Value;
            cmbUnvan.Text = bilgiGuncelle.Unvan.ToString();

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle(this.Controls);
            Temizle(groupBox1.Controls);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            bilgiGuncelle = PersonelDoldur(bilgiGuncelle);
            lstPersoneller.Items.RemoveAt(secilenIndex);
            lstPersoneller.Items.Insert(secilenIndex, ListviewDoldur(bilgiGuncelle));
            Temizle(this.Controls);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lstPersoneller.SelectedItems.Count > 0)
            {
                lstPersoneller.Items.RemoveAt(secilenIndex);
                Temizle(this.Controls);
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
            else
                MessageBox.Show("Seçim yapmadýnýz");
        }

        private void btnResimSec_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Personel Resmi(png,jpg,gif)|*.png;*.jpg;*.gif";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                pbResim.Image = Image.FromFile(ofd.FileName);
                pbResim.Tag = Path.GetExtension(ofd.FileName);
            }
        }
    }
}

