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
        public int Year { get; set; }
        public string Artist { get; set; }

        public Song(int id, string name, int year ,string artist)
        {
            SongId = id;
            NameSong = name;
            Year = year;
            Artist = artist;
        }
    }
}
