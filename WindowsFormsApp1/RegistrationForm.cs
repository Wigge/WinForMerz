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
    public partial class RegistrationForm : Form
    {

        string myConnectionstring = "Data Source=DESKTOP-RP68OGU; Initial Catalog = WinFormer; Integrated Security=True";

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.None;
            pictureBox2.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            var username = textBox1.Text;
            var password = textBox4.Text;

           bool userExist = CheckIfUserExist(username);

           if (userExist != true || username == "" || password == "")
           {
                pictureBox2.Visible = true;
                return;
           }
           else
           {

            using (var conn = new SqlConnection(myConnectionstring))
            {
                try
                {
                    conn.Open();
                    string sql = "insert into Users values (@Username,@Password);";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);//Presuming column 1 is an int
                        cmd.Parameters.AddWithValue("@Password", password); //Presuming column 2 is a varchar (string)
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }
             }
          }
          this.Close();
        }

        public bool CheckIfUserExist(string username)
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
                            if (username == reader.GetString(1))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
