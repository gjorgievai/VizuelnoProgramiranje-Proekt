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
        public Questions(Song current)
        {
            InitializeComponent();
            song = current;
            years.Add(song.Year);
            years.Add(song.Year - 2);
            years.Add(song.Year - 6);
        }
        public Questions()
        {
            InitializeComponent();
            years.Add(song.Year);
            years.Add(song.Year - 2);
            years.Add(song.Year - 6);
           
        }

        private void Questions_Load(object sender, EventArgs e)
        {
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

        private void btnSong1_Click(object sender, EventArgs e)
        {
            if (btnSong1.Text == song.Year.ToString())
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                Close();
            }

        }

        private void btnSong2_Click(object sender, EventArgs e)
        {
            if (btnSong2.Text == song.Year.ToString())
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                Close();
            }

        }

        private void btnSong3_Click(object sender, EventArgs e)
        {
            if (btnSong3.Text == song.Year.ToString())
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                Close();
            }
        }
    }
}
