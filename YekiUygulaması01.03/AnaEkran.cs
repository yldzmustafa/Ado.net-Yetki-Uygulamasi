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
    public partial class AnaEkran : Form
    {
        int yetki;
        public AnaEkran(int a)
        {
            InitializeComponent();
            this.yetki = a;
        }

        private void AnaEkran_FormClosing(object sender, FormClosingEventArgs e)        //anasayfayı kapattııktan sonra giriş sayfasını açar.
        {
            Form1 fff = new Form1();
            fff.Show();
        }

        private void AnaEkran_Load(object sender, EventArgs e)
        {
            Label lbl1 = new Label();
            Label lbl2 = new Label();
            TextBox t1 = new TextBox();
            TextBox t2 = new TextBox();
            Button btn1 = new Button();
            Button btn2 = new Button();
            Button btn3 = new Button();
            Button btn4 = new Button();
            Button btn5 = new Button();
            DataGridView dgw = new DataGridView();

            lbl1.Location = new System.Drawing.Point(100, 20);
            lbl1.Size = new System.Drawing.Size(40, 30);
            lbl1.Text = "Ad:";
            Controls.Add(lbl1);

            lbl2.Location = new System.Drawing.Point(100, 50);
            lbl2.Size = new System.Drawing.Size(40, 30);
            lbl2.Text = "Soyad:";
            Controls.Add(lbl2);

            t1.Location = new System.Drawing.Point(150, 20);
            t1.Size = new System.Drawing.Size(100, 30);
            Controls.Add(t1);

            t2.Location = new System.Drawing.Point(150, 50);
            t2.Size = new System.Drawing.Size(100, 30);
            Controls.Add(t2);

            btn1.Location = new System.Drawing.Point(100, 100);
            btn1.Size = new System.Drawing.Size(100, 30);
            btn1.Text = "Ekle";
            Controls.Add(btn1);

            btn2.Location = new System.Drawing.Point(210, 100);
            btn2.Size = new System.Drawing.Size(100, 30);
            btn2.Text = "Ara";
            Controls.Add(btn2);

            btn3.Location = new System.Drawing.Point(320, 100);
            btn3.Size = new System.Drawing.Size(100, 30);
            btn3.Text = "Güncelle";
            Controls.Add(btn3);

            btn4.Location = new System.Drawing.Point(430, 100);
            btn4.Size = new System.Drawing.Size(100, 30);
            btn4.Text = "Sil";
            Controls.Add(btn4);

            btn5.Location = new System.Drawing.Point(540, 100);
            btn5.Size = new System.Drawing.Size(100, 30);
            btn5.Text = "Tüm Liste";
            Controls.Add(btn5);

            dgw.Location = new System.Drawing.Point(80, 150);
            dgw.Size = new System.Drawing.Size(650, 250);
            Controls.Add(dgw);

            if (yetki == 2)
            {
                btn1.Enabled = false;
                btn5.Enabled = false;

            }
            else if (yetki == 3)
            {
                btn2.Enabled = false;
                btn3.Enabled = false;
                btn4.Enabled = false;
            }


            string baglanticumlesi = "Data Source=DESKTOP-1VO3R9G;Initial Catalog=Kurs;Integrated Security=True";
            string sorgu = "";

            using (SqlConnection baglanti=new SqlConnection())
            {
                baglanti.ConnectionString = baglanticumlesi;
                baglanti.Open();
                using (SqlCommand listele=new SqlCommand())
                {

                }
            }

        }
    }
}
