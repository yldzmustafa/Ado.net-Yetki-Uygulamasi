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

namespace YekiUygulaması01._03
{
    public partial class anaekran1 : Form
    {
        int yetkiid = 0;
        public anaekran1(int yetki)
        {
            InitializeComponent();
            this.yetkiid = yetki;
        }
        
        TextBox t1 = new TextBox();
        TextBox t2 = new TextBox();
        TextBox t3 = new TextBox();
        DataGridView dgw = new DataGridView();

        List<string> yetkiler = new List<string>();

        string baglanticumlesi = "Data Source=DESKTOP-1VO3R9G;Initial Catalog=Kurs;Integrated Security=True";


        private void anaekran1_Load(object sender, EventArgs e)
        {

            #region Label Oluşturma
            for (int i = 1; i < 3; i++)
            {
                Label Abc = new Label();
                
                Abc.Top = i * 30;
                Abc.Left = 90;
                Abc.Width = 75;
                Abc.TextAlign = ContentAlignment.MiddleLeft;
                switch (i)
                {
                    case 1:
                        Abc.Text = "Adı";
                        break;
                    case 2:
                        Abc.Text = "Soyadı";
                        break;
                    default:
                        break;
                }
                this.Controls.Add(Abc);
                //Abc.BackColor = Color.Lime;
                //Abc.ForeColor = Color.Black;
                Abc.Font = new Font("Arial", 12, FontStyle.Bold);

                //TextBox t1 = new TextBox();
                //t1.Location = new System.Drawing.Point(172, 75);
                //t1.Name = "textbox1";
                //t1.Size = new System.Drawing.Size(100, 20);
                //Controls.Add(t1);
            }
            //this.BackColor = Color.White;
            #endregion

            #region Yetkileri çekme
            //string komutcumlesi="select username,password,yetki from users";
            using (SqlConnection baglanti = new SqlConnection())
            {
                baglanti.ConnectionString = baglanticumlesi;
                using (SqlCommand listelemekomutu = new SqlCommand())
                {
                    baglanti.Open();
                    listelemekomutu.Connection = baglanti;
                    listelemekomutu.CommandType = CommandType.Text;
                    listelemekomutu.CommandText = "select yetkidetayAD from permisiondetail where yetkiid=@yetkiid";
                    listelemekomutu.Parameters.AddWithValue("@yetkiid", yetkiid);
                    using (SqlDataReader okuyucu = listelemekomutu.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            //MessageBox.Show(okuyucu["yetkidetayAD"].ToString());
                            yetkiler.Add(okuyucu["yetkidetayAD"].ToString());
                        }
                    }
                    baglanti.Close();
                }
            }
            #endregion

            #region Butonları Oluşturma
            Button[] buttons = new Button[5];
            int y = 0;
            for (int i = 0; i < 5; i++)
            {
                buttons[i] = new Button();
                //buttons[i].Text = "btn" + i.ToString();
                buttons[i].Size = new System.Drawing.Size(100, 30);
                buttons[i].Location = new System.Drawing.Point(152 + y, 125);
                buttons[i].Font = new Font("Arial", 12, FontStyle.Bold);
                buttons[i].FlatStyle = FlatStyle.Flat;
                buttons[i].FlatAppearance.BorderSize = 1;
                switch (i)
                {
                    case 0:
                        buttons[i].Text = "Ekle";
                        buttons[i].Click += new EventHandler(Ekle);
                        //buttons[i].Visible = yetkiler.Contains("Hepsi") ? true : yetkiler.Contains("Ekle");
                        buttons[i].Enabled = yetkiler.Contains("Hepsi") ? true : yetkiler.Contains("Ekle");
                        break;
                    case 1:
                        buttons[i].Text = "Ara";
                        buttons[i].Click += new EventHandler(Ara);
                        //buttons[i].Visible = yetkiler.Contains("Hepsi") ? true : yetkiler.Contains("Arama");
                        buttons[i].Enabled = yetkiler.Contains("Hepsi") ? true : yetkiler.Contains("Arama");
                        break;
                    case 2:
                        buttons[i].Text = "Guncelle";
                        buttons[i].Click += new EventHandler(Guncelle);
                        //buttons[i].Visible = yetkiler.Contains("Hepsi") ? true : yetkiler.Contains("Guncelle");
                        buttons[i].Enabled = yetkiler.Contains("Hepsi") ? true : yetkiler.Contains("Güncelle");
                        break;
                    case 3:
                        buttons[i].Text = "Sil";
                        buttons[i].Click += new EventHandler(Sil);
                        //buttons[i].Visible = yetkiler.Contains("Hepsi") ? true : yetkiler.Contains("Silme");
                        buttons[i].Enabled = yetkiler.Contains("Hepsi") ? true : yetkiler.Contains("Silme");
                        break;
                    case 4:
                        buttons[i].Text = "Liste";
                        buttons[i].Click += new EventHandler(Listele);
                        //buttons[i].Visible = yetkiler.Contains("Hepsi") ? true : yetkiler.Contains("Listeleme");
                        buttons[i].Enabled = yetkiler.Contains("Hepsi") ? true : yetkiler.Contains("Listeleme");
                        break;
                    default:
                        break;

                }
                Controls.Add(buttons[i]);
                y = y + 120;
            }
            #endregion

            #region TextBox Oluşturma
            t1.Location = new System.Drawing.Point(172, 75);
            t1.Name = "textBox1";
            t1.Size = new System.Drawing.Size(100, 20);
            Controls.Add(t1);

            t2.Location = new System.Drawing.Point(172, 35);
            t2.Name = "textBox2";
            t2.Size = new System.Drawing.Size(100, 20);
            Controls.Add(t2);

            t3.Location = new System.Drawing.Point(300, 75);
            t3.Name = "textBox2";
            t3.Size = new System.Drawing.Size(100, 20);
            t3.Visible = false;
            Controls.Add(t3);
            #endregion

            #region DataGridView Oluşturma
            dgw.Location = new System.Drawing.Point(152, 180);
            dgw.Size = new System.Drawing.Size(550, 200);
            dgw.CellClick += Dgw_CellClick;
            Controls.Add(dgw);
            #endregion

            #region Ogr Tablosunu listeleyen kod
            string sorgu = "select * from ogr_table";
            using (SqlConnection baglanti = new SqlConnection())
            {
                baglanti.ConnectionString = baglanticumlesi;

                using (SqlCommand listele = new SqlCommand(sorgu, baglanti))
                {
                    baglanti.Open();
                    using (DataTable dataTable = new DataTable())
                    {
                        dataTable.Columns.Add("Ogrenciid");
                        dataTable.Columns.Add("Ad");
                        dataTable.Columns.Add("Soyadı");

                        using (SqlDataReader reader = listele.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DataRow row = dataTable.NewRow();
                                row["Ogrenciid"] = reader["ogrenciid"];
                                row["Ad"] = reader["adi"];
                                row["Soyadı"] = reader["soyadi"];
                                dataTable.Rows.Add(row);
                            }
                        }
                        dgw.DataSource = dataTable;
                    }
                    baglanti.Close();
                    dgw.Columns[0].Visible = false;
                    dgw.Columns[1].Width = 300;
                    dgw.Columns[2].Width = 300;
                }
            }

            #endregion
        }

        private void Dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            t1.Text = dgw.CurrentRow.Cells[1].Value.ToString();
            t2.Text = dgw.CurrentRow.Cells[2].Value.ToString();
            t3.Text = dgw.CurrentRow.Cells[0].Value.ToString();
        }

        private void Listele(object sender, EventArgs e)
        {
            anaekran1_Load(this, null);
            Temizle();
        }

        public void Temizle()
        {
            t1.Clear();
            t2.Clear();
            t3.Clear();
        }

        private void Sil(object sender, EventArgs e)
        {
            using (SqlConnection baglanti=new SqlConnection())
            {
                baglanti.ConnectionString = baglanticumlesi;
                baglanti.Open();
                using (SqlCommand silkomutu=new SqlCommand())
                {
                    silkomutu.Connection = baglanti;
                    silkomutu.CommandType = CommandType.Text;
                    silkomutu.CommandText = "delete from ogr_table where ogrenciid= "+t3.Text;

                    if (silkomutu.ExecuteNonQuery() == 1)
                        MessageBox.Show("Kayıt silindi");
                    else
                        MessageBox.Show("Kayıt silinemedi.");
                }
                baglanti.Close();
                anaekran1_Load(this, null);
            }
        }

        private void Guncelle(object sender, EventArgs e)
        {
            using (SqlConnection baglanti=new SqlConnection())
            {
                baglanti.ConnectionString = baglanticumlesi;
                baglanti.Open();
                using (SqlCommand guncelekomutu=new SqlCommand())
                {
                    guncelekomutu.Connection = baglanti;
                    guncelekomutu.CommandType = CommandType.Text;
                    guncelekomutu.CommandText = "update ogr_table set adi=@adi, soyadi=@soyadi where ogrenciid=@id";
                    guncelekomutu.Parameters.AddWithValue("@adi", t1.Text);
                    guncelekomutu.Parameters.AddWithValue("@soyadi", t2.Text);
                    guncelekomutu.Parameters.AddWithValue("@id", t3.Text);

                    if (guncelekomutu.ExecuteNonQuery() == 1)
                        MessageBox.Show("Kayıt Güncellendi");
                    else
                        MessageBox.Show("Kayıt Güncellenemedi");
                }
                baglanti.Close();
                anaekran1_Load(this, null);
            }
        }

        private void Ara(object sender, EventArgs e)
        {
            string sorgu = "select * from ogr_table where ogrenciid=" + t3.Text;
            using (SqlConnection baglanti = new SqlConnection())
            {
                baglanti.ConnectionString = baglanticumlesi;
                using (SqlDataAdapter adapter=new SqlDataAdapter(sorgu,baglanti))
                {
                    using (DataSet ds=new DataSet())
                    {
                        adapter.Fill(ds, "a");
                        dgw.DataSource = ds.Tables["a"];
                    }
                }
            }
        }

        private void Ekle(object sender, EventArgs e)
        {
            
            using (SqlConnection baglanti = new SqlConnection())
            {
                baglanti.ConnectionString = baglanticumlesi;
                baglanti.Open();
                using (SqlCommand eklemekomutu = new SqlCommand())
                {
                    eklemekomutu.Connection = baglanti;
                    eklemekomutu.CommandType = CommandType.Text;
                    eklemekomutu.CommandText = "insert into ogr_table(adi,soyadi) values(@adi,@soyadi)";
                    eklemekomutu.Parameters.AddWithValue("@adi", t2.Text);
                    eklemekomutu.Parameters.AddWithValue("@soyadi", t1.Text);

                    if (eklemekomutu.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Kayıt yapıldı.");
                    }
                    else
                        MessageBox.Show("Kayıt yapılamadı");
                }
                baglanti.Close();
                
            }
            anaekran1_Load(this, null);
        }
        private void anaekran1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnaEkranGecis f = new AnaEkranGecis(yetkiid);
            f.Show();
        }
    }
}
