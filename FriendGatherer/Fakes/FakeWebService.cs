using FriendWrangler.Core.Models;
using FriendWrangler.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendWrangler.Core.Fakes
{
    public class FakeWebService : IWebService
    {
        public int SleepDuration { get; set; }

        //public FakeWebService(int sleepDuration)
        //{
        //    SleepDuration = sleepDuration;
        //}

        private Task Sleep()
        {
            return Task.Delay(SleepDuration);
        }

        public async Task<Models.User[]> GetFriends(Models.User user)
        {
            await Sleep();

            return new[] {
                new User {Id = 1, Username = "Byran"},
                new User {Id = 2, Username = "Chris"},
                new User {Id = 3, Username = "Sepehr"}
            };
        }

        public async Task<Models.User> Login(string username, string password)
        {
            await Sleep();

            return new User {Id = 3, Username = "Sepehr"};
        }


        public Task SendMessage(Friend friend)
        {
            return null;
        }
    }
}
