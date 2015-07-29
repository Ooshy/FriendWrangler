using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.OS;
using Android.Widget;

namespace FriendWrangler.Classes
{
    [Activity(Label = "Friend Selection")]
    public class SelectFriendsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectFriends);
            var contactsAdapter = new ContactsAdapter(this);
            var contactsListView = FindViewById<ListView>(Resource.Id.ContactsListView);
            contactsListView.Adapter = contactsAdapter;
        }
    }
}
