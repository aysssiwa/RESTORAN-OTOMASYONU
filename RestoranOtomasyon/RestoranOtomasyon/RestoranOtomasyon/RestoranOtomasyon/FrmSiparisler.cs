using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyon
{
    public partial class FrmSiparisler : Form
    {
        public FrmSiparisler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        int id = -1,fiyat=0;
     
        public void Listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM SiparisTablosu", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
        public void Temizle()
        {
            txtMusteriAdi.Text = "";
            txtAdet.Text = "";
            lblFiyat.Text = "00.00 TL"; 
            cbMenuler.SelectedIndex = -1;
            id = -1;
            lblId.Text = "-1";
            

        }
        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmRestoranIslemleri frm = new FrmRestoranIslemleri();
            frm.Show();
            this.Hide();
        }

        private void FrmSiparisler_Load(object sender, EventArgs e)
        {
            Listele();
            OleDbCommand komut1 = new OleDbCommand("SELECT MenuAdi FROM MenuTablosu", bgl.baglanti());
            OleDbDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                cbMenuler.Items.Add(dr1[0].ToString());

            }
            bgl.baglanti().Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        int total = 0;
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                total = fiyat * int.Parse(txtAdet.Text);
                lblFiyat.Text = total.ToString() + " TL";
            }
            catch
            {

            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtAdet.Text.Equals("") && !txtMusteriAdi.Text.Equals("") && !cbMenuler.Text.Equals(""))
                {
                    OleDbCommand komut = new OleDbCommand("Insert Into SiparisTablosu (MusteriAdi,MenuAdi,Miktar,ToplamFiyat,SiparisTarihi) VALUES (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", txtMusteriAdi.Text);
                    komut.Parameters.AddWithValue("@p2", cbMenuler.Text);
                    komut.Parameters.AddWithValue("@p3", txtAdet.Text);
                    komut.Parameters.AddWithValue("@p4", int.Parse(txtAdet.Text)*fiyat);
                    komut.Parameters.AddWithValue("@p5", System.DateTime.Today);
                    komut.ExecuteNonQuery();
                    Temizle();
                    MessageBox.Show("Sipariş başarıyla kaydedildi.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtMusteriAdi.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                cbMenuler.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                txtAdet.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
                lblFiyat.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
                id = int.Parse(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
            }
            catch { }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblId.Text.Equals("-1"))
                {
                    MessageBox.Show("Lütfen silinecek olan siparişi seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OleDbCommand komut = new OleDbCommand("DELETE FROM SiparisTablosu WHERE SiparisId=@p1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", id);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Sipariş Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (!txtMusteriAdi.Text.Equals("") && !txtAdet.Text.Equals("") && !cbMenuler.Text.Equals(""))
                {
                    OleDbCommand komut=new OleDbCommand("UPDATE SiparisTablosu SET MusteriAdi=@p1,MenuAdi=@p2,Miktar=@p3,ToplamFiyat=@p4,SiparisTarihi=@p5 WHERE SiparisId=@p6", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1",txtMusteriAdi.Text);
                    komut.Parameters.AddWithValue("@p2",cbMenuler.Text);
                    komut.Parameters.AddWithValue("@p3",txtAdet.Text);
                    komut.Parameters.AddWithValue("@p4",int.Parse(txtAdet.Text) * fiyat);
                    komut.Parameters.AddWithValue("@p5",System.DateTime.Today);
                    komut.Parameters.AddWithValue("@p6",id);
                    komut.ExecuteNonQuery();
                    Temizle();
                    MessageBox.Show("Sipariş başarıyla güncellendi.", "Güncelleme Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cbMenuler_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("SELECT  Fiyat FROM MenuTablosu WHERE MenuAdi=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cbMenuler.Text);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                fiyat = int.Parse(dr[0].ToString());
                lblFiyat.Text = fiyat.ToString()+" TL";
            }
            bgl.baglanti().Close();    
           
        }
    }
}
