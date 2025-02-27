using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyon
{
    public partial class FrmStok : Form
    {
        public FrmStok()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        int id = -1;
        public void Listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM StokTablosu", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
        public void Temizle()
        {
            id = -1;
            lblId.Text = "-1";
            txtFiyat.Text = "";
            txtMiktar.Text = "";
            txtUrunAdi.Text = "";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmRestoranIslemleri frm=new FrmRestoranIslemleri();
            frm.Show();
            this.Hide();
        }

        private void FrmStok_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int adet = 0;
                double fiyat = 0;
                string adi = txtUrunAdi.Text;
                try
                {
                    fiyat = double.Parse(txtFiyat.Text);
                }
                catch
                {
                    MessageBox.Show("Fiyatı sayılarla giriniz");
                }
                try
                {
                    adet = int.Parse(txtMiktar.Text);
                }
                catch
                {
                    MessageBox.Show("Adeti sayılarla giriniz");
                }
                if(!adi.Equals("")&& adet>0 && fiyat > 0)
                {
                    OleDbCommand komut = new OleDbCommand("Insert Into StokTablosu (UrunAdi,Miktar,Fiyat) VALUES (@p1,@p2,@p3)", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", adi);
                    komut.Parameters.AddWithValue("@p2", adet);
                    komut.Parameters.AddWithValue("@p3", fiyat);
                    komut.ExecuteNonQuery();
                    Temizle();
                    MessageBox.Show("Ürün başarıyla eklendi.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele();
                    bgl.baglanti().Close();
                }
                else
                {
                    MessageBox.Show("Lütfen tüm bilgileri doldurunuz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                lblId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
                txtUrunAdi.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                txtMiktar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                txtFiyat.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
                id = int.Parse(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
            }
            catch
            {

            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblId.Text.Equals("-1"))
                {
                    MessageBox.Show("Lütfen silinecek olan personeli seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OleDbCommand komut = new OleDbCommand("DELETE FROM StokTablosu WHERE UrunId=@p1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", id);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Listele();
                    Temizle();
                    bgl.baglanti().Close();
                }
            }
            catch
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int adet = 0;
                double fiyat = 0;
                string adi = txtUrunAdi.Text;
                try
                {
                    fiyat = double.Parse(txtFiyat.Text);
                }
                catch
                {
                    MessageBox.Show("Fiyatı sayılarla giriniz");
                }
                try
                {
                    adet = int.Parse(txtMiktar.Text);
                }
                catch
                {
                    MessageBox.Show("Adeti sayılarla giriniz");
                }
                if (!adi.Equals("") && adet > 0 && fiyat > 0)
                {
                    OleDbCommand komut = new OleDbCommand("UPDATE StokTablosu SET UrunAdi=@p1,Miktar=@p2,Fiyat=@p3 WHERE UrunId=@p4", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1",adi);
                    komut.Parameters.AddWithValue("@p2",adet);
                    komut.Parameters.AddWithValue("@p3",fiyat);
                    komut.Parameters.AddWithValue("@p4", id);
                    komut.ExecuteNonQuery();
                    Temizle();
                    MessageBox.Show("Ürün başarıyla güncellendi.", "Güncelleme Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele();
                    bgl.baglanti().Close();
                }
                else
                {
                    MessageBox.Show("Lütfen tüm bilgileri doldurunuz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch {
                MessageBox.Show("Beklenmeyen bir hata oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FrmEkle frm=new FrmEkle();
            frm.id = id;
            frm.Show();
            this.Hide();

        }
    }
}
