using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendWrangler.Core.Classes
{
    public class Timer
    {
        #region Fields

        private int _waitTime;
        private bool _isRunning;

        #endregion

        #region Properties

        public int WaitTime
        {
            get { return _waitTime; }
            set { _waitTime = value; }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }

        #endregion

        #region Events

        public event EventHandler Elapsed;

        #endregion

        #region Methods

        protected virtual void OnTimerElapsed()
        {
            if (Elapsed != null)
            {
                Elapsed(this, new EventArgs());
            }
        }

        public async Task Start()
        {
            int seconds = 0;
            IsRunning = true;
            while (IsRunning)
            {
                if (seconds != 0 && seconds % WaitTime == 0)
                {
                    OnTimerElapsed();
                }
                await Task.Delay(1000);
                seconds++;
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }

        #endregion

        #region Constructors

        public Timer(int waitTime)
        {
            WaitTime = waitTime;
        }

        #endregion  
    }
}
