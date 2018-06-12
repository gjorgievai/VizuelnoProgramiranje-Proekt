using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame
{
    public class Song
    {
        public int SongId { get; set; }
        public string NameSong { get; set; }
        public int YearSong { get; set; }

        public Song(int id, string name, int year)
        {
            SongId = id;
            NameSong = name;
            YearSong = year;
        }
    }
}
