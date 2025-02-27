using System;
using System.Collections;
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
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        int id = -1;

        public void Listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM MenuTablosu", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
        public void Temizle()
        {
            txtFiyat.Text = "";
            txtMenuAd.Text = "";
            txtTeslim.Text = "";
            txtTur.Text = "";
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
        
        private void FrmMenu_Load(object sender, EventArgs e)
        {
            Listele();

            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtMenuAd.Text.Equals("") && !txtFiyat.Text.Equals("") && !txtTeslim.Text.Equals("") && !txtTur.Text.Equals(""))
                {
                    
                    OleDbCommand komut = new OleDbCommand("Insert Into MenuTablosu (MenuAdi,Fiyat,Tur,OrtalamaTeslim) VALUES (@p1,@p2,@p3,@p4)", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", txtMenuAd.Text);
                    komut.Parameters.AddWithValue("@p2", txtFiyat.Text);
                    komut.Parameters.AddWithValue("@p3", txtTur.Text);
                    komut.Parameters.AddWithValue("@p4", txtTeslim.Text);
                    
                    komut.ExecuteNonQuery();
                    Temizle();
                    MessageBox.Show("Menü başarıyla eklendi.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtMenuAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                txtFiyat.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                txtTur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
                txtTeslim.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
                id = int.Parse(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
            }
            catch { 
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblId.Text.Equals("-1"))
                {
                    MessageBox.Show("Lütfen silinecek olan menüyü seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OleDbCommand komut = new OleDbCommand("DELETE FROM MenuTablosu WHERE MenuId=@p1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", id);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Menü Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (lblId.Text.Equals("-1"))
                {
                    MessageBox.Show("Lütfen güncellenecek olan menüyü seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!txtMenuAd.Text.Equals("") && !txtFiyat.Text.Equals("") && !txtTeslim.Text.Equals("") && !txtTur.Text.Equals(""))
                    {
                        OleDbCommand komut = new OleDbCommand("UPDATE MenuTablosu SET MenuAdi=@p1,Fiyat=@p2,Tur=@p3,OrtalamaTeslim=@p4 WHERE MenuId=@p5", bgl.baglanti());
                        komut.Parameters.AddWithValue("@p1", txtMenuAd.Text);
                        komut.Parameters.AddWithValue("@p2", txtFiyat.Text);
                        komut.Parameters.AddWithValue("@p3", txtTur.Text);
                        komut.Parameters.AddWithValue("@p4", txtTeslim.Text);
                        komut.Parameters.AddWithValue("@p5", id);
                        komut.ExecuteNonQuery();
                        Temizle();
                        MessageBox.Show("Menü başarıyla güncellendi.", "Güncelleme Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Listele();
                        bgl.baglanti().Close();
                    }
                    else
                    {
                        MessageBox.Show("Lütfen tüm bilgileri doldurunuz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        

        

    }
}
