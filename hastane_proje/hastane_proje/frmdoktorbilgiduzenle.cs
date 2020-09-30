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
    public partial class frmdoktorbilgiduzenle : Form
    {
        public frmdoktorbilgiduzenle()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string tc;
        private void frmdoktorbilgiduzenle_Load(object sender, EventArgs e)
        {
            mtxttc.Text = tc;
            SqlCommand komut = new SqlCommand("select * from tablodoktorlar where DoktorTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mtxttc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                textBox1.Text = dr[1].ToString();
                txtsoyad.Text = dr[2].ToString();
                combobrans.Text = dr[3].ToString();
                txtsifre.Text = dr[5].ToString();

            }
            bgl.baglanti().Close();
        }

        private void btnbilgiguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tablodoktorlar set DoktorAdi=@p1, DoktorSoyadi=@p2, DoktorBrans=@p3, DoktorSifre=@p4 where DoktorTC=@p5", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",textBox1.Text);
            komut.Parameters.AddWithValue("@p2",txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3",combobrans.Text);
            komut.Parameters.AddWithValue("@p4",txtsifre.Text);
            komut.Parameters.AddWithValue("@p5", mtxttc.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi.");
        }
    }
}
