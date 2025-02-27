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
    public partial class FrmPersoneller : Form
    {
        public FrmPersoneller()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        int id = -1;
        public void Listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM PersonelTablosu", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
        public void Temizle()
        {
            txtAd.Text = "";
            txtAdres.Text = "";
            txtSoyad.Text = "";
            txtTel.Text = "";
            txtUnvan.Text = "";
            dtpDogumGunu.Text = System.DateTime.Now.ToString();
            id= -1;
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

        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtAd.Text.Equals("") && !txtSoyad.Text.Equals("") && !txtTel.Text.Equals("") && !txtUnvan.Text.Equals(""))
                {
                    OleDbCommand komut = new OleDbCommand("Insert Into PersonelTablosu (Ad,Soyad,Telefon,Unvan,Adres,DogumTarihi) VALUES (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1",txtAd.Text );
                    komut.Parameters.AddWithValue("@p2",txtSoyad.Text );
                    komut.Parameters.AddWithValue("@p3",txtTel.Text );
                    komut.Parameters.AddWithValue("@p4", txtUnvan.Text);
                    komut.Parameters.AddWithValue("@p5", txtAdres.Text);
                    komut.Parameters.AddWithValue("@p6", dtpDogumGunu.Value.ToString());
                    komut.ExecuteNonQuery();
                    Temizle();
                    MessageBox.Show("Personel başarıyla eklendi.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
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
                    OleDbCommand komut = new OleDbCommand("DELETE FROM PersonelTablosu WHERE PersonelId=@p1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", id);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Personel Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Listele();
                    Temizle();
                    bgl.baglanti().Close();
                }
            }
            catch {
                MessageBox.Show("Beklenmeyen bir hata oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                lblId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
                txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                txtTel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
                txtUnvan.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
                txtAdres.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
                dtpDogumGunu.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
                id = int.Parse(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
            }
            catch
            {

            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtAd.Text.Equals("") && !txtSoyad.Text.Equals("") && !txtTel.Text.Equals("") && !txtUnvan.Text.Equals(""))
                {
                    OleDbCommand komut = new OleDbCommand("UPDATE PersonelTablosu SET Ad=@p1,Soyad=@p2,Telefon=@p3,Unvan=@p4,Adres=@p5,DogumTarihi=@p6 WHERE PersonelId=@p7", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", txtAd.Text);
                    komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                    komut.Parameters.AddWithValue("@p3", txtTel.Text);
                    komut.Parameters.AddWithValue("@p4", txtUnvan.Text);
                    komut.Parameters.AddWithValue("@p5", txtAdres.Text);
                    komut.Parameters.AddWithValue("@p6", dtpDogumGunu.Value.ToString());
                    komut.Parameters.AddWithValue("@p7", id);
                    komut.ExecuteNonQuery();
                    Temizle();
                    MessageBox.Show("Personel başarıyla güncellendi.", "Güncelleme Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
