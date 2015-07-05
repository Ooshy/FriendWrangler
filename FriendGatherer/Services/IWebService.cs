using FriendWrangler.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendWrangler.Core.Services
{
    public interface IWebService
    {
        Task<User[]> GetFriends(User user);
        Task<User> Login(string username, string password);

        Task SendMessage(Friend friend);
    }
}
