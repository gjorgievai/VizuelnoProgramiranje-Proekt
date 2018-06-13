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

        public string currentSongPlaying { get; private set; }
        public List<string> btnNames{ get; set; }
        public List<string> usedNames = new List<string>();
       

        public GameForm()
        {
            //playSong();
            btnNames = new List<string> { "You", "Inner City Blues", "Trouble Man",
            "I Wish", "Think", "Don't you worry 'bout a thing", "Myenemy", "Smooth Operaton",
            "Sweetest Taboo", "Clouds"};
            usedNames = btnNames;
            InitializeComponent();
            //Check Commit and Push - Ana!
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            playSong();
            refreshButtonNames();
            if (usedNames.Count > 0)
            {
                foreach (String s in usedNames)
                {
                    usedNames.Remove(s);
                    btnNames.Add(s);
                }
            }
        }

        private void playSong()
        {
            var songs = new List<string> { "You.mp3", "Inner City Blues.mp3", "Trouble Man.mp3" };
            int index = r.Next(songs.Count);
            currentSongPlaying = songs[index];
            player.URL = currentSongPlaying;
        }

        private void clearButtons()
        {
            foreach(Button b in this.Controls.OfType<Button>())
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
            btnNames.Remove(btnCorrectAnswer.Text);
            usedNames.Add(btnCorrectAnswer.Text);
            int counter = 0;
            bool flag = true;

            foreach (Button b in this.Controls.OfType<Button>())
            {
                counter++;
                
                if (btnCorrectAnswer.Name != b.Name)
                {
                    if (counter == 6)
                    {
                        b.Text = nameRandom(flag);
                    }
                    else
                    {
                        b.Text = nameRandom(!flag);
                    }
                        
                   
                }
                else if (btnCorrectAnswer.Name == b.Name)
                {
                    b.Text = btnCorrectAnswer.Text;
                }
            }
            playSong();
        }

        public string nameRandom(bool flag)
        {
            if(flag)
            {
                int j = r.Next(0, btnNames.Count);
                string Name = btnNames[j];
                btnNames.Remove(Name);
                usedNames.Add(Name);
                if (usedNames.Count > 0)
                {
                    foreach (String s in usedNames)
                    {
                        usedNames.Remove(s);
                        btnNames.Add(s);
                    }
                }
                return Name;
            }
            int i = r.Next(0, btnNames.Count);
            string name = btnNames[i];
            btnNames.Remove(name);
            usedNames.Add(name);
            return name;
        }


        private void guessSong(string currentSongPlaying, string btnSong, Button b)
        {
            string songPlaying = currentSongPlaying.Substring(0, currentSongPlaying.Length - 4);
            if (songPlaying.Equals(btnSong))
            {
                player.controls.stop();
                refreshButtonNames();
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

        


    }
}
