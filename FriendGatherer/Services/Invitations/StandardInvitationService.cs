using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FriendWrangler.Core.Enumerations;
using FriendWrangler.Core.Models;

namespace FriendWrangler.Core.Services.Invitations
{
    public class StandardInvitationService : IInvitationService 
    {
        private int PendingInvitations
        {
            get { return _pendingInvitations; }
            set { _pendingInvitations = value < 0 ? 0 : _pendingInvitations; }
        }
        private int _pendingInvitations;
        public int Limit { get; set; }
        
        public StandardInvitationService()
        {
            PendingInvitations = 0;
        }

        //I want to change this so it returns the final list of initations, but I don't know how.
        public void SendInvitations(Models.Invitation invitation, IList<Models.Friend> friends, double waitTime ,int TargetTotalGuests,string message, int limit = 1 )
        {
            Console.WriteLine("Sending inviations");
            //List to keep invitations
            var invitationList = new List<Invitation>();
            //max number of people to contact at a time
            Limit = limit;
            //Populate the Friend invitation list
            foreach (var friend in friends)
            {
                Console.WriteLine("Populating Friends");
                var friendInvitation = invitation.Clone(friend);
                Task.Factory.StartNew(() => friendInvitation.SetTimeout(waitTime));
                friendInvitation.invitationStatusChanged += InvitationOnInvitationStatusChanged;
                invitationList.Add(friendInvitation);

                
            }
            //While we have not reached the target number of people we want to invite, and there are still people
            //pending or not yet sent, do stuff!
            while (invitationList.Count(x => x.Status == InvitationStatus.Yes) != TargetTotalGuests 
                || invitationList.Count(x => x.Status == InvitationStatus.Pending) != 0 
                && invitationList.Count(x => x.Status == InvitationStatus.NotYetSent) != 0)
            {
                //while we have not reached the limit of people we want pending, send messages
                while (invitationList.Count(x => x.Status == InvitationStatus.Pending) != limit)
                {
                     //might want to keep track of priority, but this might work if list is not shuffeled.
                    //find the first person not yet send and send to them. 

                    invitationList.First(x => x.Status == InvitationStatus.NotYetSent).SendMessage(message);
                    
                } 
            }

        }

        public void InvitationOnInvitationStatusChanged(Invitation invitation, EventArgs eventArgs)
        {
            // custom logic
            switch (invitation.Status)
            {
                case InvitationStatus.Yes:
                    break;
                case InvitationStatus.No:
                    break;
                case InvitationStatus.NoResponse:
                    break;
                case InvitationStatus.NotYetSent:
                    break;
                case InvitationStatus.Unknown:
                    break;
            }

            // friend has responded to the invitation. 
            // allow the service to send an invitation to the next friend
            switch (invitation.Status)
            {
                case InvitationStatus.Yes:
                case InvitationStatus.No: 
                case InvitationStatus.Unknown:
                    PendingInvitations -= 1;
                    break;
            }
            
        }


        public Task SendInvitations(Invitation invitation, IList<Friend> friends, int limit)
        {
            throw new NotImplementedException();
        }
    }
}
