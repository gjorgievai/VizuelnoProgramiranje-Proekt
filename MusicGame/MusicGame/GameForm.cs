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
using WMPLib;

namespace MusicGame
{
    public partial class GameForm : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        Random r = new Random();

        SignIn signIn = new SignIn();
        Login login = new Login();

        User activeUser { get; set; }
        public Song song;
        public SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|MusicGameDb.mdf;Integrated Security=True");
        public SqlCommand command = new SqlCommand();
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter();
        public User active { get; set; }
        public List<Song> songs { get; set; }
        public string currentSongPlaying { get; set; }
        public List<string> btnNames { get; set; }
        public List<string> usedNames;
        int points = 0;
        public int seconds = 30;
        public int minutes = 2;
        public int misses = 0;
        public int levels = 1;
        public int hits = 0;

        public GameForm()
        {
            InitializeComponent();
            songs = new List<Song>();
            btnNames = new List<string>();
            usedNames = new List<string>();
            
            DataBind();

            //lblLevel.Text = string.Format("Level:{0}", levels);
            Login login = new Login();
                //Check Commit and Push - Ana!
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            SignIn signIn = new SignIn();
            DialogResult resultSignIn = signIn.ShowDialog();
            if(resultSignIn == DialogResult.OK)
            {
                DialogResult dialog = MessageBox.Show("Good job! You're in! Please log in now!", "LogIn", MessageBoxButtons.OK);
                if(dialog == DialogResult.OK)
                {

                    active = new User(signIn.active.User.UserName,signIn.active.User.Score);
                    if (login.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        lbUsername.Text = string.Format("Username:{0}", login.ActiveUser.User.UserName);
                        lbScore.Text = string.Format("Score:{0}", login.ActiveUser.User.Score);
                        playSong();
                        refreshButtonNames();
                        timer.Tick += new EventHandler(timer_Tick);
                    }

                }
            }  
        }

        public void playSong()
        {
            DataBind();
            int index = r.Next(songs.Count);
            currentSongPlaying = songs[index].NameSong;
            player.URL = currentSongPlaying;
        }


        private void refreshButtonNames()
        {
            Button btnCorrectAnswer = new Button();
            string currentSongTitle = currentSongPlaying.Substring(0, currentSongPlaying.Length - 4);
            btnCorrectAnswer.Text = currentSongTitle;
            int index = r.Next(1, 6);
            string btnCorrectAnswerName = "button" + index.ToString();
            btnCorrectAnswer.Name = btnCorrectAnswerName;

            if (usedNames.Count > 0)
            {
                foreach (string s in usedNames)
                {
                    btnNames.Add(s);
                }

                usedNames.Clear();
            }
            btnNames.Remove(btnCorrectAnswer.Text);
            usedNames.Add(btnCorrectAnswer.Text);

            foreach (Button b in this.Controls.OfType<Button>())
            {

                if (btnCorrectAnswer.Name != b.Name)
                {
                    b.Text = nameRandom();
                }
                else if (btnCorrectAnswer.Name == b.Name)
                {
                    b.Text = btnCorrectAnswer.Text;
                }
            }
            //playSong();
        }

        public string nameRandom()
        {
            int i = r.Next(btnNames.Count);
            string name = btnNames[i];
            btnNames.Remove(name);
            usedNames.Add(name);
            return name;
        }

        private void refreshGame()
        {
            playSong();
            refreshButtonNames();
            pbGuessSongTime.Value = 0;
            pbGuessSongTime.Maximum = 100;
        }


        private void guessSong(string currentSongPlaying, string btnSong, Button b)
        {
            string songPlaying = currentSongPlaying.Substring(0, currentSongPlaying.Length - 4);
            if (songPlaying.Equals(btnSong))
            {
                hits++;
                if(hits==10)
                {
                    levels++;
                }

                updatePoints();
                player.controls.stop();
                refreshGame();
            }
            else
            {
                misses++;
                b.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            guessSong(currentSongPlaying, this.button1.Text, button1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            guessSong(currentSongPlaying, this.button2.Text, button2);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            guessSong(currentSongPlaying, this.button3.Text, button3);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            guessSong(currentSongPlaying, this.button4.Text, button4);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            guessSong(currentSongPlaying, this.button5.Text, button5);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            guessSong(currentSongPlaying, this.button6.Text, button6);

        }

        private void updatePoints()
        {

            if (pbGuessSongTime.Value < 30)
            {
                points += 20;
                if (points <= 100)
                {
                    pbPoints.Value = points;
                }
                else {
                    timer.Stop();
                   
                    lbScore.Text = string.Format("Score:{0}", active.Score);

                    MessageBox.Show("YOU WIN! YOU PASS ALL SONGS", "WINNER", MessageBoxButtons.OK);
                    Close();
                }
                

            }
            else if (pbGuessSongTime.Value > 30 && pbGuessSongTime.Value < 65)
            {
                points += 10;
                if (points <= 100)
                {
                    pbPoints.Value = points;
                }
                else
                {
                    timer.Stop();
                    
                    lbScore.Text = string.Format("Score:{0}", active.Score);
                    MessageBox.Show("YOU WIN! YOU PASS ALL SONGS", "WINNER", MessageBoxButtons.OK);
                    Close();
                }
            }
            else if (pbGuessSongTime.Value > 65 && pbGuessSongTime.Value < 100)
            {
                points += 3;
                if (points <= 100)
                {
                    pbPoints.Value = points;
                }
                else
                {
                    timer.Stop();
                    updateDataBase();
                   
                    lbScore.Text = string.Format("Score:{0}", active.Score);
                    MessageBox.Show("YOU WIN! YOU PASS ALL SONGS", "WINNER", MessageBoxButtons.OK);
                    Close();
                }
            }

            else if (pbGuessSongTime.Value == 100)
            {
                timer.Stop();
                points -= 15;
                pbPoints.Value = points;
            }
            updateDataBase();

            lbScore.Text = string.Format("Score:{0}",active.Score);
            lblPoeni.Text = points.ToString();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblPoints.Text = points.ToString();
            
            if (pbGuessSongTime.Value != 100)
            {
                pbGuessSongTime.Value++;
            }
            if (pbGuessSongTime.Value == 100)
            {
                refreshGame();
            }
            if (misses == 10)
            {
                lblPoeni.Text = points.ToString();
                timer.Stop();
                updateDataBase();
                lbScore.Text = string.Format("Score:{0}", active.Score);
                MessageBox.Show("You misses 10 songs!! GAME OVER", "GAME OVER", MessageBoxButtons.OK);
                Close();
            }
            if (seconds > 0 || minutes > 0)
            {
                CheckTimer();
                
            }
            else
            {
                timer.Stop();
                updateDataBase();

            }
            
        }
        public void CheckTimer()
        {
            if (seconds > 0)
            {
                seconds--;
                label1.Text = string.Format("0{0}:{1}", minutes, seconds);
                if(seconds==0 && minutes == 0)
                {
                    updateDataBase();
                    lbScore.Text = string.Format("Score:{0}", active.Score);
                    MessageBox.Show("TIME'S UP!! GAME OVER", "GAME OVER", MessageBoxButtons.OK);
                    
                    Close();
                }

            }
            else
            {
                minutes--;
                seconds = 60;
            }
        }
        public void DataBind()
        {
            adapter.SelectCommand = new SqlCommand("SELECT * FROM [Song]", connection);
            adapter.Fill(dataSet);


            datagrid.DataSource = dataSet.Tables[0];
            for (int i = 0; i < datagrid.Rows.Count - 1; i++)
            {


                int id = Int32.Parse(datagrid.Rows[i].Cells[0].Value.ToString());
                string name = datagrid.Rows[i].Cells[1].Value.ToString();
                string artist = datagrid.Rows[i].Cells[3].Value.ToString();
                int year = Int32.Parse(datagrid.Rows[i].Cells[2].Value.ToString());
                song = new Song(id, name, year, artist);
                songs.Add(song);
                btnNames.Add(name.Substring(0, name.Length - 4));


            }

        

        }
        public void updateDataBase()
        {
            connection.Open();
            string username = active.UserName;
            SqlCommand command1 = new SqlCommand("UPDATE [User] SET [Score]="+points+" WHERE [UserName]='"+active.UserName+"'", connection);
            
            command1.ExecuteNonQuery();
           
            connection.Close();

        }
       
    }
}