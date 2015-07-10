using System;
using FriendWrangler.Core.Classes;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Widget;
using FriendWrangler.Core.Enumerations;
using ICSharpCode.SharpZipLib.Tar;
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

        }
        protected Invitation(Friend friend, int waitTime)
        {
            Status = InvitationStatus.NotYetSent;
            Friend = friend;
            Timer.Interval = waitTime;
        }

        #endregion

        #region Properties

        public System.Timers.Timer Timer { get; set; }

        public Task Task { get; set; }

        public Invitation Clone(Friend friend)
        {
            return new Invitation
            {
                Friend = friend,
                Id = Id,
                EventName = EventName,
                Status = InvitationStatus.NotYetSent,
                Timer = new System.Timers.Timer()
               
            };
        }

        public int Id { get; set; }
        public string EventName { get; set; }
        public InvitationStatus Status { get; set; }
        public Friend Friend { get; set; }

        #endregion

        #region Fields

        
        #endregion

        #region Methods

        public void SetWaitTime(int time)
        {
            Timer.Interval = time;

        }

        public void SendMessage(string message)
        {
            Console.WriteLine("Sent Invitation");
            this.Status = InvitationStatus.Pending;
            Task.Factory.StartNew(() => StartTimer());
            Friend.SendInvitation(message);
            Friend.MessageReceived += MessageReceived;
            //Task.Factory.StartNew((() => Friend.StartReceivingMessage()));
            ManualResetEvent wait = new ManualResetEvent(false);
            Thread work = new Thread(new ThreadStart(() =>
            {
                Friend.StartReceivingMessage();
            }));
            work.Start();
            Boolean signal = wait.WaitOne((int)Timer.Interval);
            if (!signal)
            {
                work.Abort();
            }
            Thread.SpinWait(10);
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void StartTimer()
        {
            Status = InvitationStatus.Pending;
            Timer.Elapsed += OnTimerElapsed;
            Timer.Start();
        }

        //Needs be be triggered by an external source
        //Each invitiation needs to subscrib to a delegate in its fiend class. Then the friend class will trigger the OnMessageReceived. This is because receiving messages changes based on the implementation.(facebook , google , text ect) 
        //When changes the status based on the analyzed sentiment then triggers status changed
        public void MessageReceived(string message)
        {
            
            Timer.Stop();
            
            var sentiment = InvitationAnalyzer.AnalyzeMessage(message);
            Console.WriteLine("Answer is " + sentiment);
            var ValidResponse = false;
            switch (sentiment)
            {
                case MessageSentiment.Yes:
                    Status = InvitationStatus.Yes;
                    ValidResponse = true;
                        break;
                case MessageSentiment.No:
                    Status = InvitationStatus.No;
                    ValidResponse = true;
                    //SendMessage("Too bad :(");
                        break;
                case MessageSentiment.Unknown:
                    Status = InvitationStatus.Unknown;
                    SendMessage("Is that a yes or no?");
                        break;
            }
            if (ValidResponse)
            {

                if (invitationStatusChanged != null)
                {
                    invitationStatusChanged(this, EventArgs.Empty);
                }
                //Unsubscribe to message received
                Friend.MessageReceived -= MessageReceived; 
            }
        }


        /// <summary>
        /// Sets the timeout for the timer
        /// </summary>
        /// <param name="time"></param>
        public void SetTimeout(double time)
        {
            Timer.Interval = time;
        }

        //Takes proper steps when timer elapses
        protected virtual void OnTimerElapsed(object o , EventArgs e)
        {
            Timer.Stop();
            
            Status = InvitationStatus.NoResponse;
            Friend.MessageReceived -= MessageReceived;
            Friend.SendInvitation("Sorry. Something came up.");
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
