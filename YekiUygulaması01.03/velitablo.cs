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
    public partial class velitablo : Form
    {
        DataGridView dataGridView1 = new DataGridView();
        BindingSource bindingSource1 = new BindingSource();
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        Button reloadButton = new Button();
        Button submitButton = new Button();

        int yetki;
        public velitablo(int a)
        {
            InitializeComponent();
            this.yetki = a;
            reloadButton.Text = "Yenileme";
            submitButton.Text = "Kaydet";

            reloadButton.Click += ReloadButton_Click;
            submitButton.Click += SubmitButton_Click;

            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Top;
            panel.AutoSize = true;
            dataGridView1.Dock = DockStyle.Fill;   //ekrana hizalar

            panel.Controls.AddRange(new Control[] { reloadButton, submitButton });
            this.Controls.AddRange(new Control[] { dataGridView1, panel });

            this.Load += new System.EventHandler(velitablo_Load);
            this.Text = "Listelenen verilerin yenilenme ve kaydedilmesi";
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            //Bindingsource nesnesindeki veri kaynağı datable ile dönüşüm yaptık.
            //Sonra dataadapter nesnesinin update metodu ile güncelleme yaptık.
            dataAdapter.Update((DataTable)bindingSource1.DataSource);
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            GetData(dataAdapter.SelectCommand.CommandText);
        }

        private void GetData(string selectCommand)
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-1VO3R9G;Initial Catalog=Kurs;Integrated Security=True ";

                dataAdapter = new SqlDataAdapter(selectCommand, connectionString);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataTable table = new DataTable();

                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;
            }
            catch (SqlException)
            {
                MessageBox.Show("Bu uygulama,varsayılan connectionString değişkeni sistem için geçersizdir.");
            }
        }

        private void velitablo_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bindingSource1; 
            GetData("select * from Veli");
        }

        private void velitablo_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnaEkranGecis ff = new AnaEkranGecis(yetki);
            ff.Show();
        }
    }
}
