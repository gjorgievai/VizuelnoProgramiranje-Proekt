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
    public partial class SignUp : Form
    {
        public SqlConnection connection = new SqlConnection("Data Source=IVANAKAJTAZOVA\\TEW_SQLEXPRESS;Initial Catalog=MusicDataBase;Integrated Security=True");
        public SqlCommand command = new SqlCommand();
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter();
        public User user;
        public SignUp()
        {
            InitializeComponent();
        }



        private void btnSign_Click_1(object sender, EventArgs e)
        {
            adapter.SelectCommand = new SqlCommand("SELECT * FROM [User]", connection);
            adapter.Fill(dataSet);
            List<User> users = new List<User>();

            dataGridView1.DataSource = dataSet.Tables[0];
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {


                int id1 = Int32.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                string name = dataGridView1.Rows[i].Cells[1].Value.ToString();
                int score = Int32.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                users.Add(new User(id1, name, score));



            }
            int id = users.Count + 1;
            user = new User(id, tbUserName.Text, 0);
            command = new SqlCommand("INSERT INTO [User] ([Id],[UserName],[Score]) VALUES (" + id + ",'" + tbUserName.Text + "',0)", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            DialogResult = DialogResult.OK;

        }
    }
    }
