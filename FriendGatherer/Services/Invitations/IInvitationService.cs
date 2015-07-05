using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendWrangler.Core.Models;

namespace FriendWrangler.Core.Services.Invitations
{
    public interface IInvitationService
    {
        void SendInvitations(Models.Invitation invitation, IList<Models.Friend> friends, int waitTime,
            int TargetTotalGuests, string message, int limit = 1);

    }
}
