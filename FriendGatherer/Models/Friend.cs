using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendWrangler.Core.Classes;
using FriendWrangler.Core.Constants;
using FriendWrangler.Core.Enumerations;

namespace FriendWrangler.Core.Models
{
    public abstract class Friend
    {     
        #region Properties

        public delegate void ReceiveMessageEventHandler(string message);

        public event ReceiveMessageEventHandler MessageReceived;  

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Invitation> Log { get; set; }


        public IList<Invitation> ActiveInvitations
        {
            get { return Log.Where(i => i.Status == InvitationStatus.NoResponse).ToList(); }
        }
        public IList<Invitation> OldInvitations
        {
            get { return Log.Where(i => i.Status != InvitationStatus.NoResponse ).ToList(); }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// A More effective way of adding invitations to the list
        /// </summary>
        /// <param name="invitation"></param>
        public void AddInvitation(Invitation invitation)
        {
            invitation.Friend = this;
            Log.Add(invitation);
        }

        public void StartReceivingMessage()
        {
            if (MessageReceived != null) MessageReceived(ReceiveMessages());
        }

        public abstract void SendInvitation(string message);
        public abstract string ReceiveMessages();
        #endregion


        #region Constructors

        protected Friend()
        {

        }

        protected Friend(string FirstName, string LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        

        #endregion
        
    }
}
