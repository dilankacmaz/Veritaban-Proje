using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursveritabani
{
    public partial class Form3 : Form
    {

        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=Kurs;Uid=root;Pwd=!!Dk6372!!;");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        public Form3()
        {
            InitializeComponent();
        }
        void VeriGetir()
        {
            dt = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter("SELECT * FROM ogretmen", conn);
            adapter.Fill(dt);
            dgwListe3.DataSource = dt;
            conn.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            VeriGetir();
        }
        private void dgwListe3_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtogretmenNo.Text = dgwListe3.CurrentRow.Cells[0].Value.ToString();
                txtogretmenad.Text = dgwListe3.CurrentRow.Cells[1].Value.ToString();
                txtogretmensoyad.Text = dgwListe3.CurrentRow.Cells[2].Value.ToString();
                txtogretmensinif.Text = dgwListe3.CurrentRow.Cells[3].Value.ToString();



            }
            catch
            {

            }
        }

        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("AddTeacher", conn); 
            cmd.CommandType = CommandType.StoredProcedure;

            
            cmd.Parameters.AddWithValue("@ogretmenad", txtogretmenad.Text);
            cmd.Parameters.AddWithValue("@ogretmensoyad", txtogretmensoyad.Text);
            cmd.Parameters.AddWithValue("@idsinif", int.Parse(txtogretmensinif.Text));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            VeriGetir();
            MessageBox.Show("Kayıt eklendi");

        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("DeleteTeacher", conn); 
            cmd.CommandType = CommandType.StoredProcedure;

            
            cmd.Parameters.AddWithValue("@idogretmen", int.Parse(txtogretmenNo.Text));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            VeriGetir();
            MessageBox.Show("Kayıt silindi");
        }

        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("UpdateTeacher", conn); 
            cmd.CommandType = CommandType.StoredProcedure;

            
            cmd.Parameters.AddWithValue("@idogretmen", int.Parse(txtogretmenNo.Text));
            cmd.Parameters.AddWithValue("@ogretmenad", txtogretmenad.Text);
            cmd.Parameters.AddWithValue("@ogretmensoyad", txtogretmensoyad.Text);
            cmd.Parameters.AddWithValue("@ogretmensinif", int.Parse(txtogretmensinif.Text));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            VeriGetir();
            MessageBox.Show("Kayıt güncellendi");
        }

        private void btnanasayfa_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();

            this.Hide();

            form4.Show();
        }

        private void dgwListe3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}