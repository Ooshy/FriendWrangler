using FriendWrangler.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FriendWrangler.Core.ViewModels
{
    public class BaseViewModel
    {
        protected readonly IWebService Service = ServiceContainer.Resolve<IWebService>();
        protected readonly ISettings Settings = ServiceContainer.Resolve<ISettings>();

        public event EventHandler IsBusyChanged = delegate { };

        private bool _isBusy = false;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                IsBusyChanged(this, EventArgs.Empty);
            }
        }

    }
}
