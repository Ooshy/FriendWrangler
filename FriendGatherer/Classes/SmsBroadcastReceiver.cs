using Android.App;
using Android.Content;
using Android.Net;
using Android.Runtime;
using Android.Telephony.Gsm;

namespace FriendWrangler.Classes
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
            
        }

        public string Message { get; set; }
    }
}