using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Runtime;
using Android.Telephony.Gsm;
using Android.Text;
using FriendWrangler.Core;
using FriendWrangler.Droid.Classes;
using Couchbase.Lite;
using Environment = Android.Provider.Settings.System;

namespace FriendWrangler.Droid
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new string[] { ConnectivityManager.ConnectivityAction })]
    public class SmsBroadcastReceiver : BroadcastReceiver
    {
        //public delegate void MessageReceivedEventHandler(string message, string contactinfo);
        //public event MessageReceivedEventHandler Received;
        private const string Tag = "SMSBroadcastReceiver";
        private const string IntentAction = "android.provider.Telephony.SMS_RECEIVED";

        public SmsBroadcastReceiver() : base()
        {
            
        }

        public override void OnReceive(Context context, Intent intent)
        {
            if (!intent.Action.Equals(IntentAction)) return;
            var bundle = intent.Extras;
            if (bundle == null) return;

            var pdus = bundle.Get("pdus"); 
            var castedPdus = JNIEnv.GetArray<Java.Lang.Object>(pdus.Handle);
            var messages = new SmsMessage[castedPdus.Length];
            for (var i = 0; i < castedPdus.Length; i++)
            {             
                var bytes = new byte[JNIEnv.GetArrayLength(castedPdus[i].Handle)];
                JNIEnv.CopyArray(castedPdus[i].Handle, bytes);
                messages[i] = SmsMessage.CreateFromPdu(bytes);
            }

            string messageFrom = "";
            string messageBody = "";
            foreach (var message in messages)
            {
                messageFrom = message.DisplayOriginatingAddress;
                messageBody = message.MessageBody;
            }
            var manager = Manager.SharedInstance;
            var database = manager.GetDatabase("temp");
            Console.WriteLine("Got message");
            var properties = new Dictionary<string, object>()
                {
                 {"message", messageBody},
                };


            
            
            var document = database.GetExistingDocument(messageFrom);
            
            
            if (document == null)
            {
             document = database.GetDocument(messageFrom);
            var revision = document.PutProperties(properties);
                
            }
            
            



            Console.WriteLine("Retrieved document: " + messageBody);
            //foreach (var kvp in retrievedDocument.Properties)
            //{
            //    Console.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
            //}


            
        }

        public string Message { get; set; }
    }
}