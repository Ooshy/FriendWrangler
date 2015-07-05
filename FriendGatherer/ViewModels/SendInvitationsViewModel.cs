using FriendWrangler.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendWrangler.Core.ViewModels
{
    public class SendInvitationViewModel : BaseViewModel
    {
        public Friend[] FriendsInvited { get; set; }
        public int DurationInSeconds { get; set; }
        public int AskThisManyAtATimeCount { get; set; }

        public async void SendMessage()
        {
            if (FriendsInvited != null && !FriendsInvited.Any())
            {
                IsBusy = true;
                try
                {
                    var acceptedCount = 0;
                    var askedCount = 0;
                    foreach (var friend in FriendsInvited)
                    {
                        await Service.SendMessage(friend);
                        askedCount++;

                        if (askedCount >= AskThisManyAtATimeCount)
                        {
                            // wait this long before asking the next friend
                            await Task.Delay(DurationInSeconds);
                            askedCount--;
                        }
                        // check to see if they replied positively
                        // InviteCount++
                        if (acceptedCount == FriendsInvited.Count()) break;
                    }
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                throw new Exception("No friends selected.");
            }
        }
    }
}
