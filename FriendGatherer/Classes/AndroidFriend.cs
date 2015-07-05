using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Telephony;
using Couchbase.Lite;
using FriendWrangler.Core.Models;

namespace FriendWrangler.Droid.Classes
{
    public class AndroidFriend : Friend 
    {
        public string PhoneNumber { get; set; }

        private string TextMessage { get; set; }
        public override void SendInvitation(string message)
        { 
            SmsManager.Default.SendTextMessage(PhoneNumber , null , message , null ,null);
        }

        public override string ReceiveMessages()
        {
            TextMessage = null;
            var receiver = new SmsBroadcastReceiver();
            var manager = Manager.SharedInstance;
            var database = manager.GetDatabase("temp");
            while (TextMessage == null)
            {

              var x = database.GetExistingDocument(PhoneNumber);
                if (x != null)
                {
              TextMessage =  x.Properties.Values.First().ToString();
                    x.Purge();
                    
                }
                



            }
            Console.WriteLine(TextMessage);
            return TextMessage;
        }
        public void SetProp(string message , string number)
        {
            if (number == PhoneNumber)
            {
                TextMessage = message;
            }   
        }
    }
}