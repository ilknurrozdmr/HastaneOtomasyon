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
    public partial class frmsekreterdetay : Form
    {
        public frmsekreterdetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void frmsekreterdetay_Load(object sender, EventArgs e)
        {
            lbltc.Text = tc;
            //Ad soyad
            SqlCommand komut1 = new SqlCommand("select SekreterAdSoyad from sekretertablosu where SekreterTC=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",lbltc.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while(dr1.Read())
            {
                lbladsoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            //Branşları datagride aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Bransad from branstablosu", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //doktorları listeye aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2=new SqlDataAdapter("select (DoktorAdi+' '+DoktorSoyadi) as Doktorlar,DoktorBrans from tablodoktorlar",bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource=dt2;

            //Branşları combobaxa çekme
            SqlCommand komut2 = new SqlCommand("select Bransad from branstablosu", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
                combobrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into randevutablo (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values(@p1,@p2,@p3,@p4)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@p1",msktarih.Text);
            komutkaydet.Parameters.AddWithValue("@p2",msksaat.Text);
            komutkaydet.Parameters.AddWithValue("@p3",combobrans.Text);
            komutkaydet.Parameters.AddWithValue("@p4", combodoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevunuz Oluşturuldu.");
        }

        private void combobrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            combodoktor.Items.Clear();
            SqlCommand komut=new SqlCommand("select DoktorAdi,DoktorSoyadi from tablodoktorlar where DoktorBrans=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", combobrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                combodoktor.Items.Add(dr[0] + " "+ dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void btnduyuruolustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into duyurulartablosu (Duyuru) values(@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", rchduyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyurunuz Oluşturuldu.");
        }

        private void btndoktorpanel_Click(object sender, EventArgs e)
        {
            frmdoktorpaneli frm = new frmdoktorpaneli();
            frm.Show();
        }

        private void btnbranspanel_Click(object sender, EventArgs e)
        {
            frmbranspaneli frm = new frmbranspaneli();
            frm.Show();
        }

        private void btnrandevulistesi_Click(object sender, EventArgs e)
        {
            frmrandevulistesi frm = new frmrandevulistesi();
            frm.Show();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            frmduyurular frm=new frmduyurular();
            frm.Show();
        }
    }
}
