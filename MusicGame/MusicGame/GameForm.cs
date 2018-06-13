using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public string currentSongPlaying { get; set; }
        public List<string> btnNames { get; set; }
        public List<string> usedNames;
        int points = 0;


        public GameForm()
        {
            btnNames = new List<string> { "You", "Inner City Blues", "Trouble Man",
            "I Wish", "Sir Duke", "Superstition", "Signed, Sealed, Delivered I'm Yours",
                "Boogie on Reggae Woman","For Once in My Life", "Living for the City",
            "Master Blaster (Jammin')"};
            usedNames = new List<string>();
            InitializeComponent();
            //Check Commit and Push - Ana!
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            playSong();
            refreshButtonNames();
            timer.Tick += new EventHandler(timer_Tick);

        }

        public void playSong()
        {
            var songs = new List<string> { "You.mp3", "Inner City Blues.mp3", "Trouble Man.mp3",
            "Superstition.mp3", "Sir Duke.mp3", "I Wish.mp3", "Signed, Sealed, Delivered I'm Yours.mp3",
            "Boogie on Reggae Woman.mp3", "For Once in My Life.mp3", "Living for the City.mp3", "Master Blaster (Jammin').mp3"};
            int index = r.Next(songs.Count);
            currentSongPlaying = songs[index];
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
            string songPlaying = currentSongPlaying.Substring(0, currentSongPlaying.Length - 4);
            if (songPlaying.Equals(btnSong))
            {
                updatePoints();
                player.controls.stop();
                refreshGame();
            }
            else
            {
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
                pbPoints.Value = points;

            }
            else if (pbGuessSongTime.Value > 30 && pbGuessSongTime.Value < 65)
            {
                points += 10;
                pbPoints.Value = points;
            }
            else if (pbGuessSongTime.Value > 65 && pbGuessSongTime.Value < 100)
            {
                points += 3;
                pbPoints.Value = points;
            }

            else if (pbGuessSongTime.Value == 100)
            {
                points -= 15;
                pbPoints.Value = points;
            }
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
        }
    }
}