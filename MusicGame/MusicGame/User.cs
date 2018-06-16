using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame
{
    public class User
    {
        
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
        public User(string username, int score)
        {
            
            UserName = username;
            Score = score;
        }


    }
}
