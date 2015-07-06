using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Android.Telephony;
using Couchbase.Lite;
using FriendWrangler.Core.Models;
using ICSharpCode.SharpZipLib.Zip;

namespace FriendWrangler.Droid.Classes
{
    public class AndroidFriend : Friend 
    {
        public string PhoneNumber { get; set; }

        private string TextMessage { get; set; }

        public AndroidFriend()
        {
            var receiver = new SmsBroadcastReceiver();
            var manager = Manager.SharedInstance;
            var database = manager.GetDatabase("temp");
            var view = database.GetView("onChange");
            view.SetMap((doc, emit) =>
            {
                var phones = (string)doc["message"];
                emit("text", doc["message"]);
            }, "2");
            var dsaf = database.GetView("onChange").CreateQuery();

            var testa = dsaf.ToLiveQuery();
            //foreach (var VARIABLE in testa)
            //{
            //    foreach (var foo in VARIABLE.Document.Properties.Values)
            //    {
            //        Console.WriteLine(foo); 
            //    }

            //}
        }
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
        public void SetProp(object o , EventArgs e)
        {
            
        }
    }
}