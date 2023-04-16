using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BankaTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string hesap;
        SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DbBankaTest;Integrated Security=True");

        private void Form2_Load(object sender, EventArgs e)
        {
            LblHesapNo.Text = hesap;
            baglanti.Open();
            SqlCommand komut = new SqlCommand ("Select * From TBLKISILER where hesapno = @p1",baglanti);
            komut.Parameters.AddWithValue("@p1", hesap);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[1] + " " + dr[2];
                LblTc.Text = dr[3].ToString();
                LblTelefon.Text = dr[4].ToString();
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Gönderilen Hesabın Para Artışı
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update tblhesap set bakiye = bakiye+@P1 where hesapno = @P2", baglanti);

            komut.Parameters.AddWithValue("@P1", decimal.Parse(TxtTutar.Text));
            komut.Parameters.AddWithValue("@P2", MskHesapNo.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            

            //Gönderen Hesabın Para Azalışı
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("update tblhesap set bakiye = bakiye-@K1 where hesapno = @K2", baglanti);
            komut2.Parameters.AddWithValue("@K1", decimal.Parse(TxtTutar.Text));
            komut2.Parameters.AddWithValue("@K2", hesap);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("işlem Gerçekleşti");

            


        }
    }
}
