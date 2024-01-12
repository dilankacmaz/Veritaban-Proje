using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Kursveritabani
{
    public partial class Form2 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=Kurs;Uid=root;Pwd=!!Dk6372!!;");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        
        public Form2()
        {
            InitializeComponent();
        }
        void VeriGetir()
        {
            dt = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter("SELECT * FROM sinif", conn);
            adapter.Fill(dt);
            dgwListe.DataSource = dt;
            conn.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            VeriGetir();
        }
        private void dgwListe_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtSinifNo.Text = dgwListe.CurrentRow.Cells[0].Value.ToString();
                txtSinif.Text = dgwListe.CurrentRow.Cells[1].Value.ToString();
                txtKapasite.Text = dgwListe.CurrentRow.Cells[2].Value.ToString();

            }
            catch
            {

            }

        }
       
        
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("AddClass", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sinifismi", txtSinif.Text);
            cmd.Parameters.AddWithValue("@sinifkapasitesi", Convert.ToInt32(txtKapasite.Text));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            VeriGetir();
            MessageBox.Show("Kayıt eklendi");
        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("DeleteClass", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idsinif", Convert.ToInt32(txtSinifNo.Text));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            VeriGetir();
            MessageBox.Show("Kayit Silindi");
        }

        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("UpdateClass", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idsinif", Convert.ToInt32(txtSinifNo.Text));
            cmd.Parameters.AddWithValue("@sinifismi", txtSinif.Text);
            cmd.Parameters.AddWithValue("@sinifkapasitesi", Convert.ToInt32(txtKapasite.Text));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            VeriGetir();
            MessageBox.Show("Kayıt Güncellendi");
        }

        private void btnanasayfa_Click(object sender, EventArgs e)
        {
            
                Form4 form4 = new Form4();

                this.Hide();

                form4.Show();
            }
        
    }
}
