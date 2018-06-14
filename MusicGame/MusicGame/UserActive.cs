using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame
{
    public class UserActive
    {
       public User User { get; set; }
        public UserActive(User user)
        {
            User = user;
        }
    }
}
