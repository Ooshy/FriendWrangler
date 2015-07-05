using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Telephony;
using Couchbase.Lite;
using FriendWrangler.Core.Models;
using FriendWrangler.Core.Services;
using FriendWrangler.Core.Services.Invitations;
using FriendWrangler.Droid.Classes;
using FriendWrangler;
using FriendWrangler.Classes;

namespace FriendWrangler.Droid
{
    [Activity(Label = "Friend Gatherer",
              MainLauncher = true,
              Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        private Button _createEvent;
        private Button _pendingEvents;
        private Button _eventHistory;
        private Button _about;
        private Button _sendDebugMessage;

        public Button SendDebugMessage
        {
            get { return _sendDebugMessage ?? (_sendDebugMessage = FindViewById<Button>(Resource.Id.SendTestMessage)); }
        }
        public Button CreateEvent
        {
            get { return _createEvent ?? (_createEvent = FindViewById<Button>(Resource.Id.CreateEvent)); }
        }
        public Button PendingEvents
        {
            get { return _pendingEvents ?? (_pendingEvents = FindViewById<Button>(Resource.Id.PendingEvents)); }
        }
        public Button EventHistory
        {
            get { return _eventHistory ?? (_eventHistory = FindViewById<Button>(Resource.Id.EventHistory)); }
        }
        public Button About
        {
            get { return _about ?? (_about = FindViewById<Button>(Resource.Id.About)); }
        }

        public MainActivity()
        {
         
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var manager = Manager.SharedInstance;
            var dbName = "temp";
            var database = manager.GetDatabase(dbName);
            Console.WriteLine("Database created");

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            CreateEvent.Click += delegate
            {
                Intent intent = new Intent(this, typeof(CreateEventActivity));
                StartActivity(intent);
            };

            SendDebugMessage.Click += delegate
            {
                var test = new StandardInvitationService();
                var testinvite = new Invitation() { EventName = "Farm Party" };
                var friendlist = new List<Friend>();
                friendlist.Add(new AndroidFriend() { PhoneNumber = "5712949590" });
                Task.Factory.StartNew(() => test.SendInvitations(testinvite, friendlist, 9999999, 1, "Hello Johnny", 1));
            };
        }
    }
}


