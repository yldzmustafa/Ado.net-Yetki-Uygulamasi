using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YekiUygulaması01._03
{
    public partial class AnaEkranGecis : Form
    {
        int yetki;
        public AnaEkranGecis(int a)
        {
            this.yetki = a;
            InitializeComponent();
        }
      

        private void AnaEkranGecis_Load(object sender, EventArgs e)
        {
            Button[] buttons = new Button[2];
            int y = 0;
            for (int i = 0; i < 2; i++)
            {
                buttons[i] = new Button();
                buttons[i].Size = new System.Drawing.Size(100, 30);
                buttons[i].Location = new System.Drawing.Point(152+y, 125);
                buttons[i].Font = new Font("Arial", 12, FontStyle.Bold);
                buttons[i].FlatStyle = FlatStyle.Flat;
                buttons[i].FlatAppearance.BorderSize = 1;

                switch (i)
                {
                    case 0:
                        buttons[i].Text = "Öğrenci";
                        break;
                    case 1:
                        buttons[i].Text = "Veli";
                        break;
                }
                Controls.Add(buttons[i]);
                y = y + 120;
            }

            buttons[1].Click += AnaEkranGecis_Click;
            buttons[0].Click += AnaEkranGecis_Click1;
        }

        private void AnaEkranGecis_Click1(object sender, EventArgs e)
        {
            anaekran1 ff = new anaekran1(yetki);
            this.Hide();
            ff.Show();
        }

        private void AnaEkranGecis_Click(object sender, EventArgs e)
        {
            
            velitablo v = new velitablo(yetki);
            this.Hide();
            v.Show();
        }
    }
}
