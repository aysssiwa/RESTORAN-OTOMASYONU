using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace RestoranOtomasyon
{
    public partial class FrmEkle : Form
    {
        public FrmEkle()
        {
            InitializeComponent();
        }
        public int id;
        
        sqlbaglantisi bgl=new sqlbaglantisi();
        public void Listele()
        {
            OleDbCommand komut = new OleDbCommand("SELECT * FROM StokTablosu WHERE UrunId=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", id);
            OleDbDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                lblUrun.Text = dr[1].ToString();
                lblStok.Text = dr[2].ToString();
            }
            bgl.baglanti().Close();
        }
        private void FrmEkle_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!txtEklenecek.Text.Equals(""))
            {
               
                try
                {
                    int sayi =int.Parse(txtEklenecek.Text);
                    sayi += int.Parse(lblStok.Text);
                    OleDbCommand komut = new OleDbCommand("UPDATE StokTablosu SET Miktar=@p1 WHERE UrunId=@p2", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", sayi);
                    komut.Parameters.AddWithValue("@p2", id);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Stok güncellendi. Güncel stok:"+sayi.ToString());
                    txtEklenecek.Text = "";
                    bgl.baglanti().Close();
                    Listele();
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Beklenmeyen bir hata oluştu.");

                }
            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmStok frm = new FrmStok();
            frm.Show();
            this.Hide();
        }
    }
}
