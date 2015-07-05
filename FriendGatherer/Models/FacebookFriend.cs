using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendWrangler.Core.Models
{
    public class FacebookFriend : Friend
    {

        public override void SendInvitation(string message)
        {
            throw new NotImplementedException();
        }

        public override string ReceiveMessages()
        {
            throw new NotImplementedException();
        }
    }
}
