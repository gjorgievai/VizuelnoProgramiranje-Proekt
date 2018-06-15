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
    public partial class Login : Form
    {
        public SqlConnection connection = new SqlConnection("Data Source=USER-PC;Initial Catalog=MusicDataBase;Integrated Security=True");
        public SqlCommand command = new SqlCommand();
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter();
        public UserActive ActiveUser;
        public List<User> users;
        public Login()
        {
            InitializeComponent();
            users = new List<User>();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            adapter.SelectCommand = new SqlCommand("SELECT * FROM [User]", connection);
            adapter.Fill(dataSet);

            datagridUser.DataSource = dataSet.Tables[0];
            for (int i = 0; i < datagridUser.Rows.Count - 1; i++)
            {
                int id = Int32.Parse(datagridUser.Rows[i].Cells[0].Value.ToString());
                string username = datagridUser.Rows[i].Cells[1].Value.ToString();
                int score = Int32.Parse(datagridUser.Rows[i].Cells[2].Value.ToString());
                User user1 = new User(id, username, score);
                users.Add(user1);

            }
            foreach (User user in users)
            {
                if (user.UserName == tbUserName.Text)
                {
                    ActiveUser = new UserActive(user);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
                else
                {
                    
                }
            }
        }
    }
}
