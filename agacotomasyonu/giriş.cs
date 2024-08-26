using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ağaçotomasyonu
{
    public partial class giriş : Form
    {
        public giriş()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=agac.accdb");
        
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from KULLANICILAR where KADI=@p1 AND SIFRE=@P2", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox2.Text);
            OleDbDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                işlempaneli form = new işlempaneli();
                form.Show();
                this.Hide();
            }
            baglanti.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            bilgipaneli geri = new bilgipaneli();
            geri.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kaydolma kaydol = new kaydolma();
            kaydol.Show();
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("silmek istediğinize emin misiniz?","dikkat verileri siliyorsunuz.",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (cevap == DialogResult.Yes)
            {
                try
                {
                    OleDbCommand sil = new OleDbCommand("DELETE FROM KULLANICILAR WHERE KADI='" + textBox1.Text + "'", baglanti);
                    baglanti.Open();
                    sil.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("bu kullanıcı silinmiştir.");

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (Controls[i] is TextBox) Controls[i].Text = "";

                    }

                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                }
                baglanti.Close();
            }
            else
                MessageBox.Show("işleminiz kapatıldı.");
        }

       
    }
}
