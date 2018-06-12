using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame
{
    public class ActiveUser
    {
        public User user{ get; set; }
      
        public ActiveUser(User user1)
        {
            user = user1;
        }
    }
}
