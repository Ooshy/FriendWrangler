using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendWrangler.Core.Models
{
    public class User
    {

        public User() {}



        #region Properties

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        #endregion 
    }
}
