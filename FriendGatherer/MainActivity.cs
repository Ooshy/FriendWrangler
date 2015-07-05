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
using FriendWrangler.Core.Models;
using FriendWrangler.Core.Services;
using FriendWrangler.Core.Services.Invitations;
using FriendWrangler.Droid.Classes;

namespace FriendWrangler.Droid
{
    [Activity(Label = "FriendWrangler.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);


            button.Click += delegate
            {
                var test = new StandardInvitationService();
                var testinvite = new Invitation() { EventName = "Farm Party" };
                var friendlist = new List<Friend>();
                friendlist.Add(new AndroidFriend() { PhoneNumber = "5403080218" });
                Task.Factory.StartNew(() => test.SendInvitations(testinvite, friendlist, 9999999, 1, "Hello Johnny", 1));
            };
        }
    }
}


