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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string baglanticumlesi = "Data Source=DESKTOP-1VO3R9G;Initial Catalog=Kurs;Integrated Security=True";
        bool oldumu = false;
        int yetki = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            string komutcumlesi = "select username,password,yetki from users";
            using (SqlConnection baglanti=new SqlConnection())
            {
                baglanti.ConnectionString = baglanticumlesi;
                using (SqlCommand listelemekomutu=new SqlCommand(komutcumlesi,baglanti))
                {
                    baglanti.Open();
                    using (SqlDataReader okuyucu=listelemekomutu.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            if(okuyucu["username"].ToString()==textBox1.Text && okuyucu["password"].ToString() == textBox2.Text)
                            {
                                oldumu = true;
                                yetki = int.Parse(okuyucu["yetki"].ToString());
                                //AnaEkran ff = new AnaEkran(yetki);
                                //ff.Show();
                                //this.Hide();

                                AnaEkranGecis g = new AnaEkranGecis(yetki);
                                this.Hide();
                                g.Show();
                                break;

                                //anaekran1 ff = new anaekran1(yetki);
                                //ff.Show();
                                //this.Hide();
                                //break;
                            }
                        }
                    }
                    baglanti.Close();
                }
                if (!oldumu)
                {
                    MessageBox.Show("Hatalı Kullanıcı");
                    oldumu = false;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Space == e.KeyCode)
            {
                dataGridView1.Visible = true;
            }
            if (Keys.Escape == e.KeyCode)
            {
                dataGridView1.Visible = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'kursDataSet.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.kursDataSet.Users);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

        }
    }
}
