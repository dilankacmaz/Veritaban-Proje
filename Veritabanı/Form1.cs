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
    public partial class Form1 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;Database=Kurs;Uid=root;Pwd=!!Dk6372!!;");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        string cinsiyet;
        public Form1()
        {
            InitializeComponent();
        }

        void VeriGetir()
        {
            dt = new DataTable();
            conn.Open();
            adapter = new MySqlDataAdapter("SELECT * FROM ogrenci",conn); 
            adapter.Fill(dt);
            dgwListe.DataSource = dt;   
            conn.Close();
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {
            VeriGetir();
        }

        private void dgwListe_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtNo.Text = dgwListe.CurrentRow.Cells[0].Value.ToString();
                txtAd.Text = dgwListe.CurrentRow.Cells[1].Value.ToString();
                txtSoyad.Text = dgwListe.CurrentRow.Cells[2].Value.ToString();
                string cins = dgwListe.CurrentRow.Cells[3].Value.ToString();
                if (cins == "K")
                {
                    rbKadin.Checked = true;
                }
                else
                {
                    rbErkek.Checked = true;
                }
                dtpDogumTarihi.Text = dgwListe.CurrentRow.Cells[4].Value.ToString();
                cmbSinif.Text = dgwListe.CurrentRow.Cells[5].Value.ToString();
                txtSeviye.Text = dgwListe.CurrentRow.Cells[6].Value.ToString();

            }
            catch
            {

            }

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new MySqlCommand("AddStudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                cmd.Parameters.AddWithValue("@tarih", dtpDogumTarihi.Value);
                cmd.Parameters.AddWithValue("@sinif", cmbSinif.Text); 
                cmd.Parameters.AddWithValue("@seviye", txtSeviye.Text);
             
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt eklendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }

            VeriGetir();


        }

        private void rbKadin_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "K";
        }
        private void rbErkek_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "E";
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new MySqlCommand("DeleteStudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@no", int.Parse(txtNo.Text)); // Öğrenci numarası, varsayılan olarak txtNo TextBox'ından alınıyor

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Kayıt Silindi");
                }
                else
                {
                    MessageBox.Show("Silme işlemi başarısız!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }

            VeriGetir();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new MySqlCommand("UpdateStudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@no", int.Parse(txtNo.Text));
                cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                cmd.Parameters.AddWithValue("@tarih", dtpDogumTarihi.Value);
                cmd.Parameters.AddWithValue("@seviye", txtSeviye.Text);
                

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Kayıt Güncellendi");
                }
                else
                {
                    MessageBox.Show("Güncelleme işlemi başarısız!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }

            VeriGetir();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "ograd LIKE'" + txtAra.Text + "%'";
            dgwListe.DataSource = dv;   
        }

        private void btnanasayfa_Click(object sender, EventArgs e)
        {
           
                Form4 form4 = new Form4();

                this.Hide();

                form4.Show();
           
        }
    }
}
