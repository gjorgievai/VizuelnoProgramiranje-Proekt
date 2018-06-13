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
        public Song song;
        public SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\MusicData.mdf;Integrated Security=True");
        public SqlCommand command = new SqlCommand();
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter();
        List<Song> songs { get; set; }
          
        public Form1()
        {
            InitializeComponent();
            songs = new List<Song>();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adapter.SelectCommand = new SqlCommand("SELECT * FROM [Song]",connection);
            adapter.Fill(dataSet);
            datagrid.DataSource = dataSet.Tables[0];
            for (int i = 0; i < datagrid.Rows.Count-1; i++)
            {
                

                    int id = Int32.Parse(datagrid.Rows[i].Cells[0].Value.ToString());
                    string name = datagrid.Rows[i].Cells[1].Value.ToString();
                    string artist = datagrid.Rows[i].Cells[2].Value.ToString();
                    int year = Int32.Parse(datagrid.Rows[i].Cells[3].Value.ToString());
                    song = new Song(id, name, year, artist);
                   
                    
                
            }
            
        }
    }
}