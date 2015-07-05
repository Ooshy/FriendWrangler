using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Telephony;
using FriendWrangler.Classes;
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
            
            while (TextMessage == null)
            {
                
                Task.Delay(1000);
            }
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