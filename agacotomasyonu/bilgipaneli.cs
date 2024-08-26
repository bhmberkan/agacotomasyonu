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
    public partial class bilgipaneli : Form
    {
        public bilgipaneli()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=agac.accdb");
        public void yukle()
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM  ABS",baglanti);
            adapter.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            yukle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int al = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[al].Cells[1].Value.ToString();
            richTextBox1.Text = dataGridView1.Rows[al].Cells[5].Value.ToString();
           

            //label1.Text = dataGridView1.Rows[al].Cells[6].Value.ToString();
            pictureBox1.ImageLocation= dataGridView1.Rows[al].Cells[6].Value.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                /*DataTable tablo = new DataTable();
                baglanti.Open();
                OleDbDataAdapter listele = new OleDbDataAdapter("SELECT * FROM ABS WHERE TURU LIKE '" + textBox1.Text + "'", baglanti);
                listele.Fill(tablo);
                dataGridView1.DataSource = tablo;
                baglanti.Close();
                */
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void anamenüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bilgipaneli form = new bilgipaneli();
            form.Show();
            this.Hide();
        }

        private void stokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            giriş şifre = new giriş();
            şifre.Show();
            this.Hide();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
