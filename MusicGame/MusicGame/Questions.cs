using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicGame
{
    public partial class Questions : Form
    {
        public Song song { get; set; }
        public GameForm game { get; set; }
        List<int> years = new List<int>();
        Random r = new Random();
        public Questions()
        {
            game = new GameForm();
            song = game.current;
            years.Add(song.Year);
            years.Add(song.Year - 2);
            years.Add(song.Year - 6);
            InitializeComponent();
        }

        private void Questions_Load(object sender, EventArgs e)
        {
            MessageBox.Show(game.currentSongPlaying);
            yearRandom();
        }

        public void yearRandom()
        {
            foreach (Button b in Controls.OfType<Button>())
            {
                List<int> used = new List<int>();
                int i = r.Next(years.Count);
                int year = years[i];
                b.Text = year.ToString();
                used.Add(year);
                years.Remove(year);
            }
        }
    }
}
