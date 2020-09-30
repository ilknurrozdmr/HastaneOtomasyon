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
    public partial class frmsekretergiris : Form
    {
        public frmsekretergiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void sekretergirisyap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from sekretertablosu where SekreterTC=@p1 and SekreterSifre=@p2 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mtxttc.Text);
            komut.Parameters.AddWithValue("@p2", txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                frmsekreterdetay frs = new frmsekreterdetay();
                frs.tc = mtxttc.Text;
                frs.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC veya Şifre");
            }
            bgl.baglanti().Close();
        }
    }
}
