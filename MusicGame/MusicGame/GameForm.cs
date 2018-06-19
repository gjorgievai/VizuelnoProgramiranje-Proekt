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
        public Login login = new Login();
        User activeUser { get; set; }
        public Song song { get; set; }
        public SqlConnection connection = new SqlConnection("Data Source=IVANAKAJTAZOVA\\TEW_SQLEXPRESS;Initial Catalog=MusicDataBase;Integrated Security=True");
        public SqlCommand command = new SqlCommand();
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter();
        public List<Song> songs { get; set; }
        public string currentSongPlaying { get; set; }
        public List<string> btnNames { get; set; }
        public List<string> usedNames;
        public Song current { get; set; }
        public int highScore = 0;
        int misses=0;
        int points = 0;
        public int seconds = 30;
        public int minutes = 2;
        public int hits = 0;
        public int lives = 3;
        public SignUp signUp = new SignUp();
        public GameForm()
        {
            InitializeComponent();
            songs = new List<Song>();
            btnNames = new List<string>();
            usedNames = new List<string>();
            current = new Song();
         
            
                //Check Commit and Push - Ana!
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            
            if (login.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                activeUser = login.ActiveUser.User;
                lbUsername.Text = string.Format("Username:{0}", login.ActiveUser.User.UserName);
                lbScore.Text = string.Format("Your Score:{0}", login.ActiveUser.User.Score);
                DataBind();
                getHighScore();
                playSong();
                refreshButtonNames();
                timer.Tick += new EventHandler(timer_Tick);
            }
            else if (signUp.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                activeUser = signUp.user;
                lbUsername.Text = string.Format("Username:{0}", login.ActiveUser.User.UserName);
                lbScore.Text = string.Format("Score:{0}", login.ActiveUser.User.Score);
                label4.Text = string.Format("Points:{0}", points);
                getHighScore();
                DataBind();
                playSong();
                refreshButtonNames();
                timer.Tick += new EventHandler(timer_Tick);

            }
           

        }

        public void playSong()
        {

            int index = r.Next(songs.Count);
            current = new Song(songs[index].SongId, songs[index].NameSong, songs[index].Year, songs[index].Artist);
            currentSongPlaying = songs[index].NameSong;
            player.URL = currentSongPlaying;
        }

        private void clearButtons()
        {
            foreach (Button b in this.Controls.OfType<Button>())
            {
                b.Text = "";
            }
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
            bool flag = false;
            int count = 0;
            
            string songPlaying = currentSongPlaying.Substring(0, currentSongPlaying.Length - 4);
            if (songPlaying.Equals(btnSong))
            {

                hits++;
                if (hits == 7) {
                    Questions question = new Questions(current);
                    hits = 0;
                    if (question.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        points += 50;
                    }

                }
                updatePoints();
                player.controls.stop();
                misses = 0;
                refreshGame();
            }
            else
            {
                misses++;
                if (misses == 4) {
                    points = points / 2;
                    lives--;
                    misses = 0;
                    if(lives==0)
                    {
                        if (MessageBox.Show("Do you want to play again?", "GAME OVER", MessageBoxButtons.YesNo)== System.Windows.Forms.DialogResult.Yes)
                        {
                            Application.Restart();
                            getHighScore();
                        }
                        else
                        {
                            Close();
                        }
                    }
                }
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
                
            }
            else if (pbGuessSongTime.Value > 30 && pbGuessSongTime.Value < 65)
            {
                points += 10;
                
            }
            else if (pbGuessSongTime.Value > 65 && pbGuessSongTime.Value < 100)
            {
                points += 3;
                
            }

            else if (pbGuessSongTime.Value == 100)
            {
                timer.Stop();
                points -= 15;
                pbPoints.Value = points;
            }
            updateDataBase();
            label4.Text = string.Format("Points:{0}", points);
            lblPoeni.Text = points.ToString();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (pbGuessSongTime.Value != 100)
            {
                pbGuessSongTime.Value++;
            }
            if (pbGuessSongTime.Value == 100)
            {
                refreshGame();
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

                    label4.Text = string.Format("Points:{0}", points);
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
            dataSet = new DataSet();
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
            SqlCommand command1 = new SqlCommand("UPDATE [User] SET [Score]="+points +" WHERE [Id]="+activeUser.UserId, connection);
            connection.Open();
            command1.ExecuteNonQuery();
            connection.Close();

        }
        public void getHighScore()
        {
            adapter.SelectCommand = new SqlCommand("SELECT * FROM [User]", connection);
            dataSet = new DataSet();
            adapter.Fill(dataSet);
            highScore = 0;
            List<int> scores = new List<int>();
            datagrid1.DataSource = dataSet.Tables[0];
            for (int i = 0; i < datagrid1.Rows.Count - 1; i++)
            {

                int score = Int32.Parse(datagrid1.Rows[i].Cells[2].Value.ToString());
                scores.Add(score);
            }
            foreach (int i in scores)
            {
                if (i > highScore)
                {
                    highScore = i;
                }
            }
            lblHighScore.Text = string.Format("HighScore:{0}", highScore);
        }

        
    }
}