using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame
{
    public class ActiveUser
    {
        public int UserId { get; set; }
        public int Score { get; set; }
        public ActiveUser(int user, int score)
        {
            UserId = user;
            Score = score;
        }
    }
}
