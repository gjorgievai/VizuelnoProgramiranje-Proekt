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

namespace MusicGame
{
    public partial class Form1 : Form
    {
        public User user;
        public Form1()
        {
            InitializeComponent();
         
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string stringConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\DataBase.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM User WHERE UserName=@kajtazovai ", connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            int id = (int)reader["Id"];
                            lblUserName.Text = id.ToString();
                        }
                    }

                }

            }
                lblUserName.Text = user.UserName;

        }
    }
}
