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
        public SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\MusicDb.mdf;Integrated Security=True");
        public SqlCommand command = new SqlCommand();
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter();
        List<User> users { get; set; }
          
        public Form1()
        {
            InitializeComponent();
            users = new List<User>();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adapter.SelectCommand = new SqlCommand("SELECT * FROM [User]",connection);
            adapter.Fill(dataSet);
            for (int i = 0; i < datagrid.Rows.Count; i++)
            {
                for (int j = 0; j < datagrid.Rows[i].Cells.Count; j++)
                {

                    int id = Int32.Parse(datagrid.Rows[i].Cells[j].Value.ToString());
                    string username = datagrid.Rows[i].Cells[j].Value.ToString();
                    int score = Int32.Parse(datagrid.Rows[i].Cells[j].Value.ToString());
                    user = new User(id, username, score);
                    users.Add(user);
                }
            }
            lblUserName.Text = users[0].UserName;
        }
    }
}
