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
        public List<User> usersInBase { get; set; }
        public bool flag = false;
        public SignUp()
        {
            InitializeComponent();
            usersInBase = new List<User>();
        }



        private void btnSign_Click_1(object sender, EventArgs e)
        {
            adapter.SelectCommand = new SqlCommand("SELECT * FROM [User]", connection);
            adapter.Fill(dataSet);
            

            dataGridView1.DataSource = dataSet.Tables[0];
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {


                int id1 = Int32.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                string name = dataGridView1.Rows[i].Cells[1].Value.ToString();
                int score = Int32.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
           
                usersInBase.Add(new User(id1, name, score));



            }
            bool rez = check();
            if (rez==false)
            {


                int id = usersInBase.Count + 1;

                user = new User(id, tbUserName.Text, 0);
                command = new SqlCommand("INSERT INTO [User] ([Id],[UserName],[Score]) VALUES (" + id + ",'" + tbUserName.Text + "',0)", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                DialogResult = DialogResult.OK;
            }
          

        }
        public bool check()
        {
            foreach (User user in usersInBase)
            {
                if (user.UserName.Equals(tbUserName.Text))
                {
                    flag = true;
                }

            }
            if (flag)
            {
                MessageBox.Show("User exist","Exist",MessageBoxButtons.OK);
                return true;
                
            }
            return false;
            


        }
    }
}
