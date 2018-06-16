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
    public partial class SignIn : Form
    {
        public SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|MusicGameDb.mdf;Integrated Security=True");

        public SignIn()
        {
            InitializeComponent();
        }

        private void tbUsername_Validating(object sender, CancelEventArgs e)
        {
            if(tbUsername.Text=="")
            {
                errorProvider1.SetError(tbUsername, "You must enter your username!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
           
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO [User] VALUES (@UserName, @Score)";
            command.Parameters.AddWithValue("@UserName", tbUsername.Text);
            command.Parameters.AddWithValue("@Score", 0);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch(Exception err)
            {
                DialogResult dialog = MessageBox.Show(err.Message);
            }
            finally
            {
                connection.Close();
            }
            
        }
    }
}
