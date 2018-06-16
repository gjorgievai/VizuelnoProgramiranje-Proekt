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
        public SqlDataAdapter adapter = new SqlDataAdapter();
        public DataSet dataSet = new DataSet();
        public UserActive active { get; set; }
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
            
            SqlCommand command ;
            command = new SqlCommand("INSERT INTO [User] (UserName,Score) VALUES ('"+tbUsername.Text+ "',0)", connection);
            active = new UserActive(new User(tbUsername.Text, 0));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
           

        }
    }
}
