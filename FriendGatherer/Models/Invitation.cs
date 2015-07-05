using FriendWrangler.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendWrangler.Core.Enumerations;
using Newtonsoft.Json;

namespace FriendWrangler.Core.Models
{
    public class Invitation
    {
        #region Events
        public delegate void InvitationStatusChanged(Invitation source, EventArgs eventArgs);
        public event InvitationStatusChanged invitationStatusChanged;
        #endregion

        #region Constructors

        public Invitation() : this ( null ) {}

        protected Invitation(Friend friend)
        {
            Status = InvitationStatus.NotYetSent;
            Friend = friend;
            _timer = new Timer(0);
        }
        protected Invitation(Friend friend, int waitTime)
        {
            Status = InvitationStatus.NotYetSent;
            Friend = friend;
            _timer = new Timer(waitTime);
        }

        #endregion

        #region Properties

        public Invitation Clone(Friend friend)
        {
            return new Invitation
            {
                Friend = friend,
                Id = Id,
                EventName = EventName,
                Status = InvitationStatus.NotYetSent
               
            };
        }

        public int Id { get; set; }
        public string EventName { get; set; }
        public InvitationStatus Status { get; set; }
        public Friend Friend { get; set; }
        #endregion

        #region Fields

        readonly Timer _timer; 
        
        #endregion

        #region Methods

        public void SetWaitTime(int time)
        {
            this._timer.WaitTime = time;
        }

        public void SendMessage(string message)
        {
            this.Status = InvitationStatus.Pending;
            Friend.SendInvitation(message);
            Friend.MessageReceived += MessageReceived;
            Task.Factory.StartNew(() => Friend.StartReceivingMessage());
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public async void StartTimer()
        {
            Status = InvitationStatus.Pending;
            await _timer.Start();
        }

        //Needs be be triggered by an external source
        //Each invitiation needs to subscrib to a delegate in its fiend class. Then the friend class will trigger the OnMessageReceived. This is because receiving messages changes based on the implementation.(facebook , google , text ect) 
        //When changes the status based on the analyzed sentiment then triggers status changed
        public void MessageReceived(string message)
        {
            
            _timer.Stop();
            
            var sentiment = InvitationAnalyzer.AnalyzeMessage(message);
            switch (sentiment)
            {
                case MessageSentiment.Yes:
                    Status = InvitationStatus.Yes;
                        break;
                case MessageSentiment.No:
                    Status = InvitationStatus.No;
                        break;
                case MessageSentiment.Unknown:
                    Status = InvitationStatus.Unknown;
                        break;
            }
            if (invitationStatusChanged != null)
            {
                invitationStatusChanged(this, EventArgs.Empty);
            }
            //Unsubscribe to message received
            Friend.MessageReceived -= MessageReceived;
        }


        /// <summary>
        /// Sets the timeout for the timer
        /// </summary>
        /// <param name="time"></param>
        public void SetTimeout(int time)
        {
            _timer.WaitTime = time;
        }

        //Takes proper steps when timer elapses
        protected virtual void OnTimerElapsed()
        {
            _timer.Stop();
            Status = InvitationStatus.NoResponse;
            if (invitationStatusChanged != null)
            {
                invitationStatusChanged(this, EventArgs.Empty);
            }
        }
        #endregion

        #region AbstractMethods
        
        //public abstract Task Send();
            
        #endregion
       
    }




}
