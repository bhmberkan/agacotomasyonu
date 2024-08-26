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
    public partial class işlempaneli : Form
    {
        public işlempaneli()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=agac.accdb");
 
       

        public void bosalt()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            richTextBox1.Text = "";

        }
        public void yukle()
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * from ABS",baglanti);
            adapter.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            yukle();
            MessageBox.Show("işlemler: sil/güncelle işlemi için işlemler kısmına ağaç türü girilmelidir");
        }
        bool durum;
        void tekrarr()
        {
            baglanti.Open();
            OleDbCommand tekrar = new OleDbCommand("SELECT * FROM ABS WHERE TURU=@P1", baglanti);
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
                tekrarr();
                if (durum == true)
                {
                    OleDbCommand ekleme = new OleDbCommand("INSERT into ABS(TURU,SAYISI,BOLGE,SULAMA_MIK,BILGI,RESIM)" +
                        "values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + richTextBox1.Text + "','" + textBox6.Text + "')", baglanti);
                    baglanti.Open();
                    ekleme.ExecuteNonQuery();
                    baglanti.Close();
                    yukle();
                    bosalt();
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

        private void button3_Click(object sender, EventArgs e)
        {try
            {
                OleDbCommand gnc = new OleDbCommand("UPDATE ABS set TURU='" + textBox1.Text + "',SAYISI='" + textBox2.Text + "',BOLGE='" + textBox3.Text + "',SULAMA_MIK='" + textBox4.Text + "',BILGI='"+richTextBox1.Text+"',RESIM='"+textBox6.Text+ "' WHERE TURU='" + textBox5.Text + "'", baglanti);
                baglanti.Open();
                gnc.ExecuteNonQuery();
                baglanti.Close();
                yukle();
                bosalt();
            }
            catch(Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand sil = new OleDbCommand("DELETE FROM ABS WHERE TURU='" + textBox5.Text + "'", baglanti);
                baglanti.Open();
                sil.ExecuteNonQuery();
                baglanti.Close();
                yukle();
                bosalt();

               
            }
            catch(Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable tablo = new DataTable();
                baglanti.Open();
                OleDbDataAdapter ara = new OleDbDataAdapter("SELECT * FROM ABS WHERE TURU LIKE '"+textBox5.Text+"%'",baglanti);
                ara.Fill(tablo);
                dataGridView1.DataSource = tablo;
                baglanti.Close();

            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int al = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[al].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[al].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[al].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[al].Cells[4].Value.ToString();
            richTextBox1.Text = dataGridView1.Rows[al].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.Rows[al].Cells[6].Value.ToString();

            pictureBox1.ImageLocation = dataGridView1.Rows[al].Cells[6].Value.ToString();
            if (richTextBox1.Text=="")
            {
                MessageBox.Show("bu ağaç için bilgi bulunmamaktadır");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
           dialog= MessageBox.Show("EKLEYECEĞİNİZ RESMİN BOYUTU 184; 196 OLMALIDIR!..","RESİM EKLEME",MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                openFileDialog1.ShowDialog();
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                textBox6.Text = openFileDialog1.FileName;
            }
            else
                MessageBox.Show("resim ekleme işlemini sonlandırdınız.");
            
        }

        private void anamenüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bilgipaneli form = new bilgipaneli();
            form.Show();
            this.Close();

        }

        private void stokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            giriş giriş = new giriş();
            giriş.Show();
            this.Close();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
