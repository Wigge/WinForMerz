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
    public partial class WelcomeForm : Form
    {
        string myConnectionstring = "Data Source=DESKTOP-RP68OGU; Initial Catalog = WinFormer; Integrated Security=True";

        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            RegistrationForm regForm = new RegistrationForm();
            regForm.ShowDialog();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var username = textBox1.Text;
            var password = textBox4.Text;

           bool userExist = CheckIfUserExist(username,password);

            if (userExist == true)
            {
                this.Hide();
                AdminForm bookingForm = new AdminForm();
                bookingForm.Closed += (s, args) => this.Close();
                bookingForm.Show();
            }
            else
            {
                return;
            }
        }


        public bool CheckIfUserExist(string username,string password)
        {
            List<String> columnList = new List<String>();

            using (SqlConnection connection = new SqlConnection(myConnectionstring))
            {
                connection.Open();
                string query = "Select * from Users;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (username == reader.GetString(1) && password == reader.GetString(2))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
