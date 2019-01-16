using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();

            GenerateDatabaseList();

            //sda.SelectCommand()


        }


        private void button1_Click(object sender, EventArgs e)
        {

            var firstRow = textBox1.Text;
            var secondRow = textBox2.Text;
            var thirdRow = textBox3.Text;
            var dateRow = dateTimePicker1.Text;


            string myConnectionstring = "Data Source=DESKTOP-RP68OGU; Initial Catalog = WinFormer; Integrated Security=True";
            SqlConnection con = new SqlConnection(myConnectionstring);
            SqlCommand myCommando = new SqlCommand("Select * from Members;", con);


            using (var conn = new SqlConnection(myConnectionstring))
            {
                try
                {
                    conn.Open();
                    string sql = "insert into Members values (@Name,@LastName,@Country);";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", firstRow);//Presuming column 1 is an int
                        cmd.Parameters.AddWithValue("@LastName", secondRow); //Presuming column 2 is a varchar (string)
                        cmd.Parameters.AddWithValue("@Country", thirdRow);
                        cmd.ExecuteNonQuery();
                    }
                    GenerateDatabaseList();
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }
            }
        }


        private void GenerateDatabaseList()
        {

            string myConnectionstring = "Data Source=DESKTOP-RP68OGU; Initial Catalog = WinFormer; Integrated Security=True";
            SqlConnection con = new SqlConnection(myConnectionstring);
            SqlCommand myCommando = new SqlCommand("Select * from Members;", con);

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = myCommando;
            DataTable myDataTable = new DataTable();
            sda.Fill(myDataTable);
            BindingSource bsource = new BindingSource();

            bsource.DataSource = myDataTable;

            dataGridView1.DataSource = bsource;
            sda.Update(myDataTable);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            WelcomeForm welcomeForm = new WelcomeForm();
            welcomeForm.Closed += (s, args) => this.Close();
            welcomeForm.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
