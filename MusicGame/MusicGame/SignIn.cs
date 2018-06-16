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
        public MusicDataDataSet1 dataSet1 = new MusicDataDataSet1();
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
            connection.Open();
        
            active = new UserActive(new User(tbUsername.Text, 0));
            
            SqlCommand command ;
            command = new SqlCommand("INSERT INTO [User] ([UserName], [Score]) VALUES (@UserName,@Score)", connection);
            command.Parameters.Add("@UserName", SqlDbType.Text).Value = tbUsername.Text;
            command.Parameters.Add("@Score",SqlDbType.Int).Value=0;
 
           
            command.ExecuteNonQuery();
            connection.Close();
           

        }
    }
}
