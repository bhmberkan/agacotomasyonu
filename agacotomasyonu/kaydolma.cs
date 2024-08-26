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
    public partial class kaydolma : Form
    {
        public kaydolma()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=agac.accdb");

        bool durum;
        void tekrar()
        {
            baglanti.Open();
            OleDbCommand tekrar = new OleDbCommand("SELECT * FROM KULLANICILAR WHERE KADI=@P1", baglanti);
            tekrar.Parameters.AddWithValue("@p1", textBox1.Text);
            OleDbDataReader oku = tekrar.ExecuteReader();
            if (oku.Read())
            {
                durum = false;
            }
            else
            {
                durum = true;
            }
                baglanti.Close();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                tekrar();
                if (durum == true)
                {
                    OleDbCommand ekleme = new OleDbCommand("INSERT INTO KULLANICILAR(KADI,SIFRE,EPOSTA,TELEFON) " +
                    "values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + maskedTextBox1.Text + "')", baglanti);
                    baglanti.Open();
                    ekleme.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("kaydınız başarılı.");
                }
                else
                    MessageBox.Show("bu kayıt zaten var!.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);

               
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            baglanti.Close();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            giriş form = new giriş();
            form.Show();
            this.Hide();
        }
    }
}
