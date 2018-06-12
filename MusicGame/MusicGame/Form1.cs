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
           
            SqlConnection connection = new SqlConnection("metadata = res://*/MusicDb.csdl|res://*/MusicDb.ssdl|res://*/MusicDb.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\DataBase.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;");
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM User",connection);

            SqlDataReader reader = command.ExecuteReader();
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    user.UserName = reader.GetString(1);
                }

            }
            lblUserName.Text = user.UserName;
        }
    }
}
