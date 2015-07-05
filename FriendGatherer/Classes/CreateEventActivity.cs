using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FriendWrangler.Classes
{
    
    [Activity(Label = "Create Event")]
    public class CreateEventActivity : Activity
    {
        private Button _selectFriends;
        private TextView _eventName;
        private TextView _eventDescription;

        public Button SelectFriends {
            get { return _selectFriends ?? (_selectFriends = FindViewById<Button>(Resource.Id.SelectFriends)); }       
        }

        public TextView EventName
        {
            get { return _eventName ?? (_eventName = FindViewById<Button>(Resource.Id.EventName)); }       
        }

        public TextView EventDescription
        {
            get { return _eventDescription ?? (_eventDescription = FindViewById<Button>(Resource.Id.EventDescription)); }
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.CreateEvent);


            SelectFriends.Click += delegate
            {
                Intent intent = new Intent(this, typeof(SelectFriendsActivity));
                StartActivity(intent);
            };
        }
    }
}