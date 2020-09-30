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

namespace hastane_proje
{
    public partial class frmbilgiduzenle : Form
    {
        public frmbilgiduzenle()
        {
            InitializeComponent();
        }
        public string tcno;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void frmbilgiduzenle_Load(object sender, EventArgs e)
        {
            mtxttc.Text = tcno;
            SqlCommand komut = new SqlCommand("select * from hastalartablosu where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mtxttc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                txtad.Text = dr[1].ToString();
                txtsoyad.Text = dr[2].ToString();
                mtxttel.Text = dr[4].ToString();
                txtsifre.Text = dr[5].ToString();
                combocinsiyet.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnbilgiguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update hastalartablosu set HastaAd=@p1, HastaSoyad=@p2,HastaTelefon=@p3, HastaSifre=@p4, HastaCinsiyet=@p5 where HastaTC=@p6", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtad.Text);
            komut2.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut2.Parameters.AddWithValue("@p3", mtxttel.Text);
            komut2.Parameters.AddWithValue("@p4", txtsifre.Text);
            komut2.Parameters.AddWithValue("@p5", combocinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6", mtxttc.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
